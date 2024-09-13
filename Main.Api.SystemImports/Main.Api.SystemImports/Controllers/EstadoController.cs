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
    public class EstadoController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly TokenService _service;

        public EstadoController(IDbConnection connection, TokenService service)
        {
            this._connection = connection;
            this._service = service;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Estados.
         */
        [HttpGet("get-page")]
        [AllowAnonymous]
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
                    command.CommandText = $@"SELECT * FROM si_estados e WHERE e.id = {estadoId}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                        {
                            var errorResponse = new BaseErrorResponse($"Nenhum estado de ID {estadoId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                            return BadRequest(errorResponse);
                        }

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
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"INSERT INTO si_estados (nome, sigla, pais_id) VALUES ('{estado.Nome}', '{estado.Sigla}', {estado.PaisId})";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"SELECT e.id FROM si_estados e WHERE e.nome = '{estado.Nome}'";
                        int newEstadoId = (int)command.ExecuteScalar();
                        estado.Id = newEstadoId;
                        return CreatedAtAction(nameof(InsertEstado), new { id = estado.Id }, estado);
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Estado foi inserido.", new HttpModel(System.Net.HttpStatusCode.BadRequest));
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
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"UPDATE si_estados SET nome = '{novoNome}' WHERE id = {estadoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"SELECT * FROM si_estados WHERE id = {estadoId}";
                        Estado tmpEstado = new Estado();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            {
                                var errorResponse = new BaseErrorResponse($"Nenhum estado de ID {estadoId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                                return BadRequest(errorResponse);
                            }

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
                        var errorResponse = new BaseErrorResponse("Nenhum estado foi atualizado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
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
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = $@"SELECT * FROM si_estados WHERE id = {estadoId}";
                    Estado tmpEstado = new Estado();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                        {
                            var errorResponse = new BaseErrorResponse($"Nenhum Estado de ID {estadoId} encontrado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
                            return BadRequest(errorResponse);
                        }

                        while (reader.Read())
                        {
                            tmpEstado.Id = int.Parse(reader["id"].ToString());
                            tmpEstado.Nome = reader["nome"].ToString();
                            tmpEstado.Sigla = reader["sigla"].ToString();
                            tmpEstado.PaisId = int.Parse(reader["pais_id"].ToString());
                        }
                    }

                    command.CommandText = $@"DELETE FROM si_estados WHERE id = {estadoId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Estado>(tmpEstado, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Estado foi deletado.", new HttpModel(System.Net.HttpStatusCode.NotFound));
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