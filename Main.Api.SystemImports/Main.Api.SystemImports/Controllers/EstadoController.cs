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
    public class EstadoController : ControllerBase
    {
        private IDbConnection _connection;
        public EstadoController(IDbConnection connection)
        {
            this._connection = connection;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Estados.
         */
        [HttpGet("get-page")]
        public IActionResult GetAllEstados([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 30, [FromQuery] string searchOrder = "asc")
        {
            try
            {
                int skipValue, takeValue;
                List<Estado> listEstados = new List<Estado>();

                this._connection.Open();

                using (var command = this._connection.CreateCommand())
                {
                    if (currentPage != 1)
                    {
                        skipValue = (currentPage - 1) * pageSize;
                        takeValue = currentPage * pageSize;

                        command.CommandText = $@"select * from si_estados limit {skipValue} offset {takeValue}";
                    }
                    else
                        command.CommandText = $@"select * from si_estados limit {pageSize}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listEstados.Add(new Estado(int.Parse(reader["id"].ToString()), reader["nome"].ToString(), reader["sigla"].ToString(), int.Parse(reader["pais_id"].ToString())));
                        }
                    }
                }

                return Ok(new BaseResponse<Estado>(listEstados, listEstados.Count, currentPage, pageSize));

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
        public IActionResult GetEstado([FromQuery][Required] int estadoId = 0)
        {
            try
            {
                this._connection.Open();
                Estado estadoResult = new Estado();

                if (estadoId == 0)
                    throw new ArgumentNullException("ID do estado não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"select * from si_estados e where e.id = {estadoId}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum estado de ID {estadoId} encontrado.");

                        while (reader.Read())
                        {
                            estadoResult.Id = int.Parse(reader["id"].ToString());
                            estadoResult.Nome = reader["nome"].ToString();
                            estadoResult.Sigla = reader["sigla"].ToString();
                            estadoResult.PaisId = int.Parse(reader["pais_id"].ToString());
                        }
                    }

                    return Ok(new BaseCrudResponse<Estado>(estadoResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));

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
        public IActionResult InsertEstado([FromBody][Required] Estado estado)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(estado.Nome))
                    throw new ArgumentNullException("Nome do estado não pode ser nulo.");

                if (string.IsNullOrEmpty(estado.Sigla))
                    throw new ArgumentNullException("Sigla do estado não pode ser nulo.");

                if (estado.PaisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"insert into si_estados (nome, sigla, pais_id) values ('{estado.Nome}', '{estado.Sigla}', {estado.PaisId})";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"select e.id from si_estados e where e.nome = '{estado.Nome}'";

                        int newEstadoId = (int)command.ExecuteScalar();
                        estado.Id = newEstadoId;
                        return CreatedAtAction(nameof(InsertEstado), new { id = estado.Id }, estado);

                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return BadRequest("Nenhum Estado foi inserido.");
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
        public IActionResult UpdateEstado([FromQuery][Required] int estadoId, [FromQuery][Required] string novoNome)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(novoNome))
                    throw new ArgumentNullException("Nome do estado não pode ser nulo.");

                if (estadoId == 0)
                    throw new ArgumentNullException("ID do estado não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"update si_estados set nome = '{novoNome}' where id = {estadoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();

                        command.CommandText = @$"select * from si_estados where id = {estadoId}";
                        Estado tmpEstado = new Estado();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                                return BadRequest($"Nenhum estado de ID {estadoId} encontrado.");

                            while (reader.Read())
                            {
                                tmpEstado.Id = int.Parse(reader["id"].ToString());
                                tmpEstado.Nome = reader["nome"].ToString();
                                tmpEstado.Sigla = reader["sigla"].ToString();
                                tmpEstado.PaisId = int.Parse(reader["pais_id"].ToString());
                            }
                        }

                        return Ok(new BaseCrudResponse<Estado>(tmpEstado, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum estado foi atualizado.");
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
        public IActionResult DeleteEstado([FromQuery][Required] int estadoId)
        {
            try
            {
                this._connection.Open();

                if (estadoId == 0)
                    throw new ArgumentNullException("ID do Estado não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Cria uma nova transação
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = @$"select * from si_estados where id = {estadoId}";
                    Estado tmpEstado = new Estado();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum estado de ID {estadoId} encontrado.");

                        while (reader.Read())
                        {
                            tmpEstado.Id = int.Parse(reader["id"].ToString());
                            tmpEstado.Nome = reader["nome"].ToString();
                            tmpEstado.Sigla = reader["sigla"].ToString();
                            tmpEstado.PaisId = int.Parse(reader["pais_id"].ToString());
                        }
                    }

                    command.CommandText = @$"delete from si_estados where id = {estadoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Estado>(tmpEstado, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum estado foi deletado.");
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