using Main.Api.SystemImports.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;

#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace Main.Api.SystemImports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private IDbConnection _connection;
        public PaisController(IDbConnection connection)
        {
            this._connection = connection;
        }

        /*
         * Responsável por retornar todos os itens da tabela de Países.
         */
        [HttpGet("get-page")]
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
        public IActionResult GetPais([FromQuery][Required] int paisId = 0)
        {
            try
            {
                this._connection.Open();

                if (paisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    command.CommandText = $@"select p.nome from si_paises p where p.id = '{paisId}'";
                    var result = command.ExecuteScalar();
                    if (result == null)
                        return BadRequest($"Nenhum país de ID {paisId} encontrado.");
                    else
                    {
                        Paises paisResult = new Paises(paisId, result.ToString());
                        return Ok(new BaseCrudResponse<Paises>(paisResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));
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
        [HttpPost("insert")]
        public IActionResult InsertPaises([FromBody][Required] Paises pais)
        {
            try
            {
                this._connection.Open();

                if (string.IsNullOrEmpty(pais.Nome))
                    throw new ArgumentNullException("Nome do país não pode ser nulo.");

                using (var command = this._connection.CreateCommand())
                {
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"insert into si_paises (nome) values ('{pais.Nome}')";
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        command.Transaction.Commit();

                        command.CommandText = $@"select p.id from si_paises p where p.nome = '{pais.Nome}'";
                        int newPaisId = (int)command.ExecuteScalar();
                        pais.Id = newPaisId;
                        return CreatedAtAction(nameof(InsertPaises), new { id = pais.Id }, pais);

                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return BadRequest("Nenhum país foi inserido.");
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
        [HttpPut("update")]
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
                    // Inicia uma nova transação no DB
                    command.Transaction = this._connection.BeginTransaction();
                    command.CommandText = $@"update si_paises set nome = '{novoNome}' where id = {paisId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        command.Transaction.Commit();
                        Paises tmpPaises = new Paises();
                        tmpPaises.Nome = novoNome;
                        tmpPaises.Id = paisId;
                        return Ok(new BaseCrudResponse<Paises>(tmpPaises, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum país foi atualizado.");
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
        public IActionResult DeletePaises([FromQuery][Required] int paisId)
        {
            try
            {
                this._connection.Open();

                if (paisId == 0)
                    throw new ArgumentNullException("ID do país não pode ser nulo ou igual a 0.");

                using (var command = this._connection.CreateCommand())
                {
                    // Cria uma nova transação
                    command.Transaction = this._connection.BeginTransaction();

                    string oldNome = string.Empty;

                    using (var command2 = this._connection.CreateCommand())
                    {
                        command2.CommandText = @$"select nome from si_paises where id = {paisId}";
                        oldNome = command2.ExecuteScalar().ToString();

                        if (string.IsNullOrEmpty(oldNome))
                            return NoContent();
                    }

                    command.CommandText = @$"delete from si_paises where id = {paisId}";
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {

                        command.Transaction.Commit();
                        Paises tmpPaises = new Paises();
                        tmpPaises.Nome = oldNome;
                        tmpPaises.Id = paisId;
                        return Ok(new BaseCrudResponse<Paises>(tmpPaises, new HttpModel((System.Net.HttpStatusCode)200)));
                    }
                    else
                    {
                        command.Transaction.Rollback();
                        return NotFound("Nenhum país foi atualizado.");
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