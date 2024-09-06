using Main.Api.SystemImports.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IDbConnection _connection;
        public UsuarioController(IDbConnection connection)
        {
            this._connection = connection;
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

                /* if (usuario.TipoUsuarioId == 0)
                    throw new ArgumentNullException("ID do Tipo de Usuário não pode ser nulo."); */

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"CALL prc_si_valida_usuario(:p_email, :p_senha, :p_empresaid, :p_retorno)";

                    /*
                     * Parâmetros de Entrada
                     */
                    var pEmail = new NpgsqlParameter("p_email", NpgsqlDbType.Text);
                    pEmail.Direction = ParameterDirection.Input;
                    pEmail.Value = usuario.Email;

                    var pSenha = new NpgsqlParameter("p_senha", NpgsqlDbType.Text);
                    pSenha.Direction = ParameterDirection.Input;
                    pSenha.Value = usuario.Senha;

                    var pEmpresaId = new NpgsqlParameter("p_empresaid", NpgsqlDbType.Integer);
                    pEmpresaId.Direction = ParameterDirection.Input;
                    pEmpresaId.Value = usuario.EmpresaId;

                    /*
                     * Parâmetro de Saída
                     */
                    var pRetorno = new NpgsqlParameter("p_retorno", NpgsqlDbType.Text);
                    pRetorno.Direction = ParameterDirection.InputOutput;
                    pRetorno.Value = DBNull.Value;

                    // Adicionando os Parâmetros na Procedure
                    command.Parameters.Add(pEmail);
                    command.Parameters.Add(pSenha);
                    command.Parameters.Add(pEmpresaId);
                    command.Parameters.Add(pRetorno);

                    command.ExecuteNonQuery();

                    // Obtendo o valor do parâmetro de saída
                    UsuarioResult = (string)pRetorno.Value;

                    return Ok(new BaseCrudResponse<string>(UsuarioResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));
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
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"insert into si_usuario (email, senha, empresa_id, tipo_usuario_id) values ('{usuario.Email}', '{usuario.Senha}', {usuario.EmpresaId}, 1)";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"select u.id from si_usuario u where u.email = '{usuario.Email}' and u.senha = '{usuario.Senha}' and u.empresa_id = {usuario.EmpresaId}";

                        int newUsuarioId = (int)command.ExecuteScalar();
                        usuario.Id = newUsuarioId;
                        usuario.TipoUsuarioId = 1;
                        return CreatedAtAction(nameof(InsertUsuario), new { id = usuario.Id }, usuario);

                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return BadRequest("Nenhum Usuário foi inserido.");
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
        public IActionResult UpdateUsuario([FromBody][Required] Usuario usuario)
        {
            try
            {
                this._connection.Open();

                if (usuario.Id == 0)
                    throw new ArgumentNullException("ID do Usuário não pode ser nula ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();

                    Usuario usuarioResult = new Usuario();

                    command.CommandText = $@"select * from si_usuario e where e.id = {usuario.Id}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null || !((NpgsqlDataReader)reader).HasRows)
                            return BadRequest($"Nenhum Usuário de ID {usuario.Id} encontrado.");

                        while (reader.Read())
                        {
                            usuarioResult.Id = int.Parse(reader["id"].ToString());
                            usuarioResult.EmpresaId = int.Parse(reader["empresa_id"].ToString());
                            usuarioResult.TipoUsuarioId = int.Parse(reader["tipo_usuario_id"].ToString());
                            usuarioResult.Email = reader["email"].ToString();
                            usuarioResult.Senha = reader["senha"].ToString();
                        }
                    }

                    /*
                     * Antes de dar Update na tabela de Enderecos, vê se tem diferença dentre um campo e outro.
                     */
                    if (!usuario.Email.Equals(usuarioResult.Email))
                        usuarioResult.Email = usuario.Email;

                    if (!usuario.Senha.Equals(usuarioResult.Senha))
                        usuarioResult.Senha = usuario.Senha;

                    command.CommandText = $@"
update si_usario set
email = '{usuarioResult.Email}',
senha = '{usuarioResult.Senha}'
where id = {usuarioResult.Id}
";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        return Ok(new BaseCrudResponse<Usuario>(usuarioResult, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum Usuário foi atualizado.");
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