using Main.Api.SystemImports.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        private IDbConnection _connection;
        public CidadeController(IDbConnection connection)
        {
            this._connection = connection;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Estados.
         */
        [HttpGet("get-page")]
        public IActionResult GetAllCidades([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 30, [FromQuery] string searchOrder = "asc")
        {
            try
            {
                int skipValue, takeValue;
                List<Cidade> listCidades = new List<Cidade>();

                this._connection.Open();

                using (var command = this._connection.CreateCommand())
                {
                    if (currentPage != 1)
                    {
                        skipValue = (currentPage - 1) * pageSize;
                        takeValue = currentPage * pageSize;

                        command.CommandText = $@"select * from si_cidades limit {skipValue} offset {takeValue}";
                    }
                    else
                        command.CommandText = $@"select * from si_cidades limit {pageSize}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listCidades.Add(new Cidade(int.Parse(reader["id"].ToString()), reader["nome"].ToString(), reader["estado_sigla"].ToString()));
                        }
                    }
                }

                return Ok(new BaseResponse<Cidade>(listCidades, listCidades.Count, currentPage, pageSize));

            }
            finally
            {
                this._connection.Close();
            }
        }

        /*
         * Responsável por retornar um  item específico da tabela de Estados.
         */
        [HttpGet("get-one")]
        public IActionResult GetCidade([FromQuery][Required] int cidadeId = 0)
        {
            try
            {
                this._connection.Open();
                Cidade cidadeResult = new Cidade();

                if (cidadeId == 0)
                    throw new ArgumentNullException("ID da Cidade não pode ser nula ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"select * from si_cidades e where e.id = {cidadeId}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhuma Cidade de ID {cidadeId} encontrada.");

                        while (reader.Read())
                        {
                            cidadeResult.Id = int.Parse(reader["id"].ToString());
                            cidadeResult.Nome = reader["nome"].ToString();
                            cidadeResult.EstadoSigla = reader["estado_sigla"].ToString();
                        }
                    }

                    return Ok(new BaseCrudResponse<Cidade>(cidadeResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));

                }
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString()); // Exceção Genérica Padrão
            }
            finally
            {
                this._connection.Close();
            }
        }

        /*
         * Responsável por retornar um país na tabela de Estados.
         */
        [HttpPost("insert")]
        public IActionResult InsertCidade([FromBody][Required] Cidade cidade)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(cidade.Nome))
                    throw new ArgumentNullException("Nome da Cidade não pode ser nula.");

                if (string.IsNullOrEmpty(cidade.EstadoSigla))
                    throw new ArgumentNullException("Sigla do estado não pode ser nulo na inserção de uma Cidade.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"insert into si_cidades (nome, estado_sigla) values ('{cidade.Nome}', '{cidade.EstadoSigla}')";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"select c.id from si_cidades c where c.nome = '{cidade.Nome}'";

                        int newCidadeid = (int)command.ExecuteScalar();
                        cidade.Id = newCidadeid;
                        return CreatedAtAction(nameof(InsertCidade), new { id = cidade.Id }, cidade);

                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return BadRequest("Nenhuma Cidade foi inserida.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString()); // Exceção Genérica Padrão
            }
            finally
            {
                this._connection.Close();
            }
        }

        /*
         * Responsável por retornar um país na tabela de Estados.
         */
        [HttpPut("update")]
        public IActionResult UpdateCidade([FromQuery][Required] int cidadeId, [FromQuery][Required] string novoNome)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(novoNome))
                    throw new ArgumentNullException("Nome do estado não pode ser nulo.");

                if (cidadeId == 0)
                    throw new ArgumentNullException("ID da Cidade não pode ser nula ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"update si_cidades set nome = '{novoNome}' where id = {cidadeId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();

                        command.CommandText = @$"select * from si_cidades where id = {cidadeId}";
                        Cidade tmpCidade = new Cidade();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                                return BadRequest($"Nenhuma Cidade de ID {cidadeId} encontrado.");

                            while (reader.Read())
                            {
                                tmpCidade.Id = int.Parse(reader["id"].ToString());
                                tmpCidade.Nome = reader["nome"].ToString();
                                tmpCidade.EstadoSigla = reader["estado_sigla"].ToString();
                            }
                        }

                        return Ok(new BaseCrudResponse<Cidade>(tmpCidade, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhuma Cidade foi atualizada.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString()); // Exceção Genérica Padrão
            }
            finally
            {
                this._connection.Close();
            }
        }

        /*
         * Responsável por retornar um país na tabela de Países.
         */
        [HttpDelete("delete")]
        public IActionResult DeleteCidade([FromQuery][Required] int cidadeId)
        {
            try
            {
                this._connection.Open();

                if (cidadeId == 0)
                    throw new ArgumentNullException("ID da Cidade não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Cria uma nova transação
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = @$"select * from si_cidades where id = {cidadeId}";
                    Cidade tmpCidade = new Cidade();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum estado de ID {cidadeId} encontrado.");

                        while (reader.Read())
                        {
                            tmpCidade.Id = int.Parse(reader["id"].ToString());
                            tmpCidade.Nome = reader["nome"].ToString();
                            tmpCidade.EstadoSigla = reader["sigla"].ToString();
                        }
                    }

                    command.CommandText = @$"delete from si_cidades where id = {cidadeId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Cidade>(tmpCidade, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhuma cidade foi deletada.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString()); // Exceção Genérica Padrão
            }
            finally
            {
                this._connection.Close();
            }
        }
    }
}
#pragma warning restore CS8604 // Possível argumento de referência nula.