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
    public class EnderecosController : ControllerBase
    {
        private IDbConnection _connection;
        public EnderecosController(IDbConnection connection)
        {
            this._connection = connection;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Enderecos.
         */
        [HttpGet("get-page")]
        public IActionResult GetAllEnderecos([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 30, [FromQuery] string searchOrder = "asc")
        {
            try
            {
                int skipValue, takeValue;
                List<Enderecos> listEnderecos = new List<Enderecos>();

                this._connection.Open();

                using (var command = this._connection.CreateCommand())
                {
                    if (currentPage != 1)
                    {
                        skipValue = (currentPage - 1) * pageSize;
                        takeValue = currentPage * pageSize;

                        command.CommandText = $@"select * from si_enderecos limit {skipValue} offset {takeValue}";
                    }
                    else
                        command.CommandText = $@"select * from si_enderecos limit {pageSize}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listEnderecos.Add(new Enderecos(int.Parse(reader["id"].ToString()), reader["rua"].ToString(), int.Parse(reader["numero"].ToString()), reader["bairro"].ToString(), reader["complemento"].ToString(), int.Parse(reader["cidade_id"].ToString())));
                        }
                    }
                }

                return Ok(new BaseResponse<Enderecos>(listEnderecos, listEnderecos.Count, currentPage, pageSize));

            }
            finally
            {
                this._connection.Close();
            }
        }

        /*
         * Responsável por retornar um  item específico da tabela de Enderecos.
         */
        [HttpGet("get-one")]
        public IActionResult GetEndereco([FromQuery][Required] int enderecoId = 0)
        {
            try
            {
                this._connection.Open();
                Enderecos enderecoResult = new Enderecos();

                if (enderecoId == 0)
                    throw new ArgumentNullException("ID do Endereço não pode ser nula ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"select * from si_enderecos e where e.id = {enderecoId}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum Endereço de ID {enderecoId} encontrado.");

                        while (reader.Read())
                        {
                            enderecoResult.Id = int.Parse(reader["id"].ToString());
                            enderecoResult.Rua = reader["nome"].ToString();
                            enderecoResult.Numero = int.Parse(reader["numero"].ToString());
                            enderecoResult.Bairro = reader["bairro"].ToString();
                            enderecoResult.Complemento = reader["complemento"].ToString();
                            enderecoResult.CidadeId = int.Parse(reader["cidade_id"].ToString());
                        }
                    }

                    return Ok(new BaseCrudResponse<Enderecos>(enderecoResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));

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
         * Responsável por retornar um país na tabela de Enderecos.
         */
        [HttpPost("insert")]
        public IActionResult InsertEndereco([FromBody][Required] Enderecos endereco)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(endereco.Rua))
                    throw new ArgumentNullException("Rua do Endreço não pode ser nulo.");

                if (endereco.Numero == 0)
                    throw new ArgumentNullException("Numero do Endereço não pode ser nulo.");

                if (string.IsNullOrEmpty(endereco.Bairro))
                    throw new ArgumentNullException("Bairro do Endreço não pode ser nulo.");

                if (endereco.CidadeId == 0)
                    throw new ArgumentNullException("Id da Cidade não pode ser nulo durante a inserção de um Endereço.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"insert into si_enderecos (rua, numero, bairro, complemento, cidade_id) values ('{endereco.Rua}', {endereco.Numero}, '{endereco.Bairro}', '{endereco.Complemento}', {endereco.CidadeId})";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"select e.id from si_enderecos e where e.rua = '{endereco.Rua}' and e.numero = {endereco.Numero}";

                        int newEnderecoId = (int)command.ExecuteScalar();
                        endereco.Id = newEnderecoId;
                        return CreatedAtAction(nameof(InsertEndereco), new { id = endereco.Id }, endereco);

                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return BadRequest("Nenhum Endereço foi inserido.");
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
         * Responsável por retornar um país na tabela de Enderecos.
         */
        [HttpPut("update")]
        public IActionResult UpdateEndereco([FromBody][Required] Enderecos endereco)
        {
            try
            {
                this._connection.Open();

                if (endereco.Id == 0)
                    throw new ArgumentNullException("ID da Endereço não pode ser nula ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();

                    Enderecos enderecoResult = new Enderecos();

                    command.CommandText = $@"select * from si_enderecos e where e.id = {endereco.Id}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum Endereço de ID {endereco.Id} encontrado.");

                        while (reader.Read())
                        {
                            enderecoResult.Id = int.Parse(reader["id"].ToString());
                            enderecoResult.Rua = reader["nome"].ToString();
                            enderecoResult.Numero = int.Parse(reader["numero"].ToString());
                            enderecoResult.Bairro = reader["bairro"].ToString();
                            enderecoResult.Complemento = reader["complemento"].ToString();
                            enderecoResult.CidadeId = int.Parse(reader["cidade_id"].ToString());
                        }
                    }

                    /*
                     * Antes de dar Update na tabela de Enderecos, vê se tem diferença dentre um campo e outro.
                     */
                    if (!endereco.Rua.Equals(enderecoResult.Rua))
                        enderecoResult.Rua = endereco.Rua;

                    if (!endereco.Bairro.Equals(enderecoResult.Bairro))
                        enderecoResult.Bairro = endereco.Bairro;

                    if (!endereco.Complemento.Equals(enderecoResult.Complemento))
                        enderecoResult.Complemento = endereco.Complemento;

                    if (!endereco.Numero.Equals(enderecoResult.Numero))
                        enderecoResult.Numero = endereco.Numero;

                    command.CommandText = $@"
update si_enderecos set
rua = '{enderecoResult.Rua}',
numero = {enderecoResult.Numero},
bairro = '{enderecoResult.Bairro}',
complemento = '{enderecoResult.Complemento}'
where id = {enderecoResult.Id}
";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Enderecos>(enderecoResult, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum Endereço foi atualizado.");
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
         * Responsável por retornar um país na tabela de Enderecos.
         */
        [HttpDelete("delete")]
        public IActionResult DeleteEndereco([FromQuery][Required] int enderecoId)
        {
            try
            {
                this._connection.Open();

                if (enderecoId == 0)
                    throw new ArgumentNullException("ID do Endereço não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Cria uma nova transação
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = @$"select * from si_enderecos where id = {enderecoId}";
                    Enderecos tmpEndereco = new Enderecos();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum Endereço de ID {enderecoId} encontrado.");

                        while (reader.Read())
                        {
                            tmpEndereco.Id = int.Parse(reader["id"].ToString());
                            tmpEndereco.Rua = reader["nome"].ToString();
                            tmpEndereco.Numero = int.Parse(reader["numero"].ToString());
                            tmpEndereco.Bairro = reader["bairro"].ToString();
                            tmpEndereco.Complemento = reader["complemento"].ToString();
                            tmpEndereco.CidadeId = int.Parse(reader["cidade_id"].ToString());
                        }
                    }

                    command.CommandText = @$"delete from si_enderecos where id = {enderecoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Enderecos>(tmpEndereco, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum Endereço foi deletado.");
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