using Main.Api.SystemImports.Models;
using Main.Api.SystemImports.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IDbConnection _connection;
        private readonly TokenService _service;

        public UsuarioController(IDbConnection connection, TokenService service)
        {
            this._connection = connection;
            this._service = service;
        }

        /*
         * Responsável por validar o usuário.
         */
        [HttpPost("valida-usuario")]
        public IActionResult ValidaUsuario([FromBody][Required] Usuario usuario)
        {
            try
            {
                this._connection.Open();
                string UsuarioResult = string.Empty;

                if (usuario == null)
                    throw new ArgumentNullException("Parâmetro não pode ser nulo.");

                if (string.IsNullOrEmpty(usuario.Email))
                    throw new ArgumentNullException("Email não pode ser nulo.");

                if (string.IsNullOrEmpty(usuario.Senha))
                    throw new ArgumentNullException("Senha não pode ser nula.");

                if (usuario.EmpresaId == 0)
                    throw new ArgumentNullException("ID da Empresa não pode ser nula.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"CALL prc_si_valida_usuario(:p_email, :p_senha, :p_empresaid, :p_retorno)";

                    var pEmail = new NpgsqlParameter("p_email", NpgsqlDbType.Text)
                    {
                        Direction = ParameterDirection.Input,
                        Value = usuario.Email
                    };

                    var pSenha = new NpgsqlParameter("p_senha", NpgsqlDbType.Text)
                    {
                        Direction = ParameterDirection.Input,
                        Value = usuario.Senha
                    };

                    var pEmpresaId = new NpgsqlParameter("p_empresaid", NpgsqlDbType.Integer)
                    {
                        Direction = ParameterDirection.Input,
                        Value = usuario.EmpresaId
                    };

                    var pRetorno = new NpgsqlParameter("p_retorno", NpgsqlDbType.Text)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = DBNull.Value
                    };

                    command.Parameters.Add(pEmail);
                    command.Parameters.Add(pSenha);
                    command.Parameters.Add(pEmpresaId);
                    command.Parameters.Add(pRetorno);

                    command.ExecuteNonQuery();

                    UsuarioResult = (string)pRetorno.Value;

                    var token = this._service.GenerateToken(usuario);
                    usuario.Senha = string.Empty;

                    var newResponse = new
                    {
                        Response = new BaseCrudResponse<string>(UsuarioResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)),
                        Token = token
                    };

                    return Ok(newResponse);
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
        public IActionResult InsertUsuario([FromBody][Required] Usuario usuario)
        {
            try
            {
                this._connection.Open();

                if (usuario == null)
                    throw new ArgumentNullException("Parâmetro não pode ser nulo.");

                if (string.IsNullOrEmpty(usuario.Email))
                    throw new ArgumentNullException("Email não pode ser nulo.");

                if (string.IsNullOrEmpty(usuario.Senha))
                    throw new ArgumentNullException("Senha não pode ser nula.");

                if (usuario.EmpresaId == 0)
                    throw new ArgumentNullException("ID da Empresa não pode ser nula.");

                using (var command = this._connection.CreateCommand())
                {
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"INSERT INTO si_usuario (email, senha, empresa_id, tipo_usuario_id) VALUES ('{usuario.Email}', '{usuario.Senha}', {usuario.EmpresaId}, 1)";

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"SELECT u.id FROM si_usuario u WHERE u.email = '{usuario.Email}' AND u.senha = '{usuario.Senha}' AND u.empresa_id = {usuario.EmpresaId}";

                        int newUsuarioId = (int)command.ExecuteScalar();
                        usuario.Id = newUsuarioId;
                        usuario.TipoUsuarioId = 1;
                        return CreatedAtAction(nameof(InsertUsuario), new { id = usuario.Id }, usuario);
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        var errorResponse = new BaseErrorResponse("Nenhum Usuário foi inserido.", new HttpModel(System.Net.HttpStatusCode.BadRequest));
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
        public IActionResult DeleteUsuario([FromQuery][Required] int usuarioId)
        {
            try
            {
                this._connection.Open();

                if (usuarioId == 0)
                    throw new ArgumentNullException("ID do Usuário não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Cria uma nova transação
                    command.Transaction = this._connection.BeginTransaction();

                    command.CommandText = @$"select * from si_usuario where id = {usuarioId}";
                    Usuario tmpUsuario = new Usuario();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum Usuário de ID {usuarioId} encontrado.");

                        while (reader.Read())
                        {
                            tmpUsuario.Id = int.Parse(reader["id"].ToString());
                            tmpUsuario.Email = reader["email"].ToString();
                            tmpUsuario.Senha = reader["senha"].ToString();
                            tmpUsuario.EmpresaId = int.Parse(reader["empresa_id"].ToString());
                            tmpUsuario.TipoUsuarioId = int.Parse(reader["tipo_usuario_id"].ToString());
                        }
                    }

                    command.CommandText = @$"delete from si_usuario where id = {usuarioId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Usuario>(tmpUsuario, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum Usuário foi deletado.");
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