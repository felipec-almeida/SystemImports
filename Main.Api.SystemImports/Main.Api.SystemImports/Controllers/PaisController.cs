using Main.Api.SystemImports.Models;
using Main.Api.SystemImports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly TokenService _service;

        public PaisController(IDbConnection connection, TokenService service)
        {
            this._connection = connection;
            this._service = service;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Países.
         */
        [HttpGet("get-page")]
        [Authorize]
        public IActionResult GetAllPaises([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 30, [FromQuery] string searchOrder = "asc")
        {
            try
            {
                int skipValue, takeValue;
                List<Paises> listPaises = new List<Paises>();

                this._connection.Open();

                using (var command = this._connection.CreateCommand())
                {
                    if (currentPage != 1)
                    {
                        skipValue = (currentPage - 1) * pageSize;
                        takeValue = currentPage * pageSize;

                        command.CommandText = $@"select * from si_paises limit {skipValue} offset {takeValue}";
                    }
                    else
                        command.CommandText = $@"select * from si_paises limit {pageSize}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listPaises.Add(new Paises(int.Parse(reader["id"].ToString()), reader["nome"].ToString()));
                        }
                    }
                }

                return Ok(new BaseResponse<Paises>(listPaises, listPaises.Count, currentPage, pageSize));

            }
            finally
            {
                this._connection.Close();
            }
        }

        [HttpGet("get-one")]
        [Authorize]
        public IActionResult GetPais([FromQuery][Required] int paisId = 0)
        {
            try
            {
                this._connection.Open();
                if (paisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"SELECT p.nome FROM si_paises p WHERE p.id = '{paisId}'";
                    var result = command.ExecuteScalar();

                    if (result == null)
                    {
                        var errorResponse = new BaseErrorResponse($"Nenhum país de ID {paisId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }

                    Paises paisResult = new Paises(paisId, result.ToString());
                    return Ok(new BaseCrudResponse<Paises>(paisResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
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
         * Responsável por retornar um país na tabela de Países.
         */
        [HttpPost("insert")]
        [Authorize]
        public IActionResult InsertPaises([FromBody][Required] Paises pais)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(pais.Nome))
                    throw new ArgumentNullException("Nome do país não pode ser nulo.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"INSERT INTO si_paises (nome) VALUES ('{pais.Nome}')";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();
                        command.CommandText = $@"SELECT p.id FROM si_paises p WHERE p.nome = '{pais.Nome}'";
                        int newPaisId = (int)command.ExecuteScalar();
                        pais.Id = newPaisId;
                        return CreatedAtAction(nameof(GetPais), new { id = pais.Id }, pais);
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum país foi inserido.", new HttpModel(System.Net.HttpStatusCode.BadRequest));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
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
         * Responsável por retornar um país na tabela de Países.
         */
        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdatePaises([FromQuery][Required] int paisId, [FromQuery][Required] string novoNome)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(novoNome))
                    throw new ArgumentNullException("Nome do país não pode ser nulo.");

                if (paisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"UPDATE si_paises SET nome = '{novoNome}' WHERE id = {paisId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        Paises tmpPaises = new Paises { Nome = novoNome, Id = paisId };
                        return Ok(new BaseCrudResponse<Paises>(tmpPaises, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum país foi atualizado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
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
         * Responsável por retornar um país na tabela de Países.
         */
        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeletePaises([FromQuery][Required] int paisId)
        {
            try
            {
                this._connection.Open();

                if (paisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    string oldNome = string.Empty;

                    command.CommandText = $@"SELECT nome FROM si_paises WHERE id = {paisId}";
                    oldNome = command.ExecuteScalar()?.ToString();

                    if (string.IsNullOrEmpty(oldNome))
                    {
                        var errorResponse = new BaseErrorResponse($"Nenhum país de ID {paisId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }

                    command.CommandText = $@"DELETE FROM si_paises WHERE id = {paisId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        Paises tmpPaises = new Paises { Nome = oldNome, Id = paisId };
                        return Ok(new BaseCrudResponse<Paises>(tmpPaises, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum país foi deletado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
                return BadRequest(errorResponse);
            }
            catch (InvalidDataException ex)
            {
                var errorResponse = new BaseErrorResponse(ex.Message, new HttpModel(System.Net.HttpStatusCode.NotFound));
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