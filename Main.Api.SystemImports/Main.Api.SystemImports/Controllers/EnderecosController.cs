using Main.Api.SystemImports.Models;
using Main.Api.SystemImports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Data;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EnderecosController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly TokenService _service;

        public EnderecosController(IDbConnection connection, TokenService service)
        {
            this._connection = connection;
            this._service = service;
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
                    throw new ArgumentNullException("ID do Endereço não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"SELECT * FROM si_enderecos e WHERE e.id = {enderecoId}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                        {
                            var errorResponse = new BaseErrorResponse($"Nenhum Endereço de ID {enderecoId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                            return BadRequest(errorResponse);
                        }

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
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
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
        [AllowAnonymous]
        public IActionResult InsertEndereco([FromBody][Required] Enderecos endereco)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(endereco.Rua))
                    throw new ArgumentNullException("Rua do Endereço não pode ser nula.");

                if (endereco.Numero == 0)
                    throw new ArgumentNullException("Número do Endereço não pode ser nulo.");

                if (string.IsNullOrEmpty(endereco.Bairro))
                    throw new ArgumentNullException("Bairro do Endereço não pode ser nulo.");

                if (endereco.CidadeId == 0)
                    throw new ArgumentNullException("ID da Cidade não pode ser nulo durante a inserção de um Endereço.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"INSERT INTO si_enderecos (rua, numero, bairro, complemento, cidade_id) VALUES ('{endereco.Rua}', {endereco.Numero}, '{endereco.Bairro}', '{endereco.Complemento}', {endereco.CidadeId})";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"SELECT e.id FROM si_enderecos e WHERE e.rua = '{endereco.Rua}' AND e.numero = {endereco.Numero}";

                        int newEnderecoId = (int)command.ExecuteScalar();
                        endereco.Id = newEnderecoId;
                        return CreatedAtAction(nameof(InsertEndereco), new { id = endereco.Id }, endereco);
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Endereço foi inserido.", new HttpModel(System.Net.HttpStatusCode.BadRequest));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
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
                    throw new ArgumentNullException("ID do Endereço não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    Enderecos enderecoResult = new Enderecos();

                    command.CommandText = $@"SELECT * FROM si_enderecos e WHERE e.id = {endereco.Id}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                        {
                            var errorResponse = new BaseErrorResponse($"Nenhum Endereço de ID {endereco.Id} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                            return BadRequest(errorResponse);
                        }

                        while (reader.Read())
                        {
                            enderecoResult.Id = int.Parse(reader["id"].ToString());
                            enderecoResult.Rua = reader["rua"].ToString();
                            enderecoResult.Numero = int.Parse(reader["numero"].ToString());
                            enderecoResult.Bairro = reader["bairro"].ToString();
                            enderecoResult.Complemento = reader["complemento"].ToString();
                            enderecoResult.CidadeId = int.Parse(reader["cidade_id"].ToString());
                        }
                    }

                    command.CommandText = $@"
UPDATE si_enderecos SET
rua = '{endereco.Rua}',
numero = {endereco.Numero},
bairro = '{endereco.Bairro}',
complemento = '{endereco.Complemento}'
WHERE id = {enderecoResult.Id}";

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Enderecos>(enderecoResult, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Endereço foi atualizado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
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
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = $@"SELECT * FROM si_enderecos WHERE id = {enderecoId}";
                    Enderecos tmpEndereco = new Enderecos();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                        {
                            var errorResponse = new BaseErrorResponse($"Nenhum Endereço de ID {enderecoId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                            return BadRequest(errorResponse);
                        }

                        while (reader.Read())
                        {
                            tmpEndereco.Id = int.Parse(reader["id"].ToString());
                            tmpEndereco.Rua = reader["rua"].ToString();
                            tmpEndereco.Numero = int.Parse(reader["numero"].ToString());
                            tmpEndereco.Bairro = reader["bairro"].ToString();
                            tmpEndereco.Complemento = reader["complemento"].ToString();
                            tmpEndereco.CidadeId = int.Parse(reader["cidade_id"].ToString());
                        }
                    }

                    command.CommandText = $@"DELETE FROM si_enderecos WHERE id = {enderecoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Enderecos>(tmpEndereco, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Endereço foi deletado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.BadRequest));
                return BadRequest(errorResponse);
            }
            finally
            {
                this._connection.Close();
            }
        }
    }
}
#pragma warning restore CS8604 // Possível argumento de referência nula.