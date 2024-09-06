using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using Main.Api.SystemImports.Models;

[ApiController]
[Route("[controller]")]
public class EmpresaController : ControllerBase
{
    private IDbConnection _connection;

    public EmpresaController(IDbConnection connection)
    {
        this._connection = connection;
    }

    /*
     * Responsável por retornar todos os itens da tabela de Empresas.
     */
    [HttpGet("get-page")]
    public IActionResult GetAllEmpresas([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 30)
    {
        try
        {
            int skipValue, takeValue;
            List<Empresa> listEmpresas = new List<Empresa>();

            this._connection.Open();

            using (var command = this._connection.CreateCommand())
            {
                skipValue = (currentPage - 1) * pageSize;
                command.CommandText = $@"SELECT * FROM si_empresas LIMIT {pageSize} OFFSET {skipValue}";

                using (var reader = command.ExecuteReader())
                {
                    if (reader == null || reader.FieldCount == 0)
                        return NotFound($"Nenhuma Empresa encontrada.");

                    while (reader.Read())
                    {
                       listEmpresas.Add(new Empresa(
                            int.Parse(reader["id"].ToString()),
                            reader["nome"].ToString(),
                            reader["descricao"].ToString(),
                            reader["cnpj"].ToString(),
                            int.Parse(reader["endereco_id"].ToString()),
                            reader["status"].ToString()));
                    }
                }
            }

            return Ok(new BaseResponse<Empresa>(listEmpresas, listEmpresas.Count, currentPage, pageSize));
        }
        finally
        {
            this._connection.Close();
        }
    }

    /*
     * Responsável por retornar um item específico da tabela de Empresas.
     */
    [HttpGet("get-one")]
    public IActionResult GetEmpresa([FromQuery][Required] int empresaId)
    {
        try
        {
            this._connection.Open();
            Empresa empresaResult = null;

            if (empresaId <= 0)
                throw new ArgumentNullException("ID da Empresa não pode ser nulo ou menor ou igual a 0.");

            using (var command = this._connection.CreateCommand())
            {
                command.CommandText = $@"SELECT * FROM si_empresas WHERE id = {empresaId}";

                using (var reader = command.ExecuteReader())
                {
                    if (reader == null || reader.FieldCount == 0)
                        return NotFound($"Nenhuma Empresa de ID {empresaId} encontrada.");

                    while (reader.Read())
                    {
                        empresaResult = new Empresa(
                            int.Parse(reader["id"].ToString()),
                            reader["nome"].ToString(),
                            reader["descricao"].ToString(),
                            reader["cnpj"].ToString(),
                            int.Parse(reader["endereco_id"].ToString()),
                            reader["status"].ToString());
                    }
                }

                return Ok(new BaseCrudResponse<Empresa>(empresaResult, new HttpModel((System.Net.HttpStatusCode)this.Response.StatusCode)));
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            this._connection.Close();
        }
    }

    /*
     * Responsável por inserir uma nova Empresa.
     */
    [HttpPost("insert")]
    public IActionResult InsertEmpresa([FromBody][Required] Empresa empresa)
    {
        try
        {
            this._connection.Open();

            if (string.IsNullOrEmpty(empresa.Nome))
                throw new ArgumentNullException("Nome da Empresa não pode ser nulo.");

            if (string.IsNullOrEmpty(empresa.Cnpj))
                throw new ArgumentNullException("CNPJ da Empresa não pode ser nulo.");

            using (var command = this._connection.CreateCommand())
            {
                command.Transaction = this._connection.BeginTransaction();
                command.CommandText = $@"INSERT INTO si_empresas (nome, descricao, cnpj, endereco_id, status) 
                                          VALUES ('{empresa.Nome}', '{empresa.Descricao}', '{empresa.Cnpj}', {empresa.EnderecoId}, {empresa.Status})";

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected != 0)
                {
                    command.Transaction.Commit();

                    command.CommandText = $@"SELECT id FROM si_empresas WHERE cnpj = '{empresa.Cnpj}'";
                    int newEmpresaId = (int)command.ExecuteScalar();
                    empresa.Id = newEmpresaId;
                    return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
                }
                else
                {
                    command.Transaction.Rollback();
                    return BadRequest("Nenhuma Empresa foi inserida.");
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            this._connection.Close();
        }
    }

    /*
     * Responsável por atualizar uma Empresa existente.
     */
    [HttpPut("update")]
    public IActionResult UpdateEmpresa([FromQuery][Required] int empresaId, [FromBody][Required] Empresa empresa)
    {
        try
        {
            this._connection.Open();

            if (empresaId <= 0)
                throw new ArgumentNullException("ID da Empresa não pode ser nulo ou menor ou igual a 0.");

            using (var command = this._connection.CreateCommand())
            {
                command.Transaction = this._connection.BeginTransaction();
                command.CommandText = $@"UPDATE si_empresas 
                                          SET nome = '{empresa.Nome}', descricao = '{empresa.Descricao}', 
                                              cnpj = '{empresa.Cnpj}', endereco_id = {empresa.EnderecoId}, 
                                              status = {empresa.Status} 
                                          WHERE id = {empresaId}";

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    command.Transaction.Commit();
                    return Ok(new BaseCrudResponse<Empresa>(empresa, new HttpModel((System.Net.HttpStatusCode)200)));
                }
                else
                {
                    command.Transaction.Rollback();
                    return NotFound("Nenhuma Empresa foi atualizada.");
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            this._connection.Close();
        }
    }

    /*
     * Responsável por deletar uma Empresa existente.
     */
    [HttpDelete("delete")]
    public IActionResult DeleteEmpresa([FromQuery][Required] int empresaId)
    {
        try
        {
            this._connection.Open();

            if (empresaId <= 0)
                throw new ArgumentNullException("ID da Empresa não pode ser nulo ou menor ou igual a 0.");

            using (var command = this._connection.CreateCommand())
            {
                command.Transaction = this._connection.BeginTransaction();

                command.CommandText = $@"SELECT * FROM si_empresas WHERE id = {empresaId}";
                Empresa tmpEmpresa = null;

                using (var reader = command.ExecuteReader())
                {
                    if (reader == null || reader.FieldCount != 0)
                        return NotFound($"Nenhuma Empresa de ID {empresaId} encontrada.");

                    while (reader.Read())
                    {
                        tmpEmpresa = new Empresa(
                            int.Parse(reader["id"].ToString()),
                            reader["nome"].ToString(),
                            reader["descricao"].ToString(),
                            reader["cnpj"].ToString(),
                            int.Parse(reader["endereco_id"].ToString()),
                            reader["status"].ToString());
                    }
                }

                command.CommandText = $@"DELETE FROM si_empresas WHERE id = {empresaId}";
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    command.Transaction.Commit();
                    return Ok(new BaseCrudResponse<Empresa>(tmpEmpresa, new HttpModel((System.Net.HttpStatusCode)200)));
                }
                else
                {
                    command.Transaction.Rollback();
                    return NotFound("Nenhuma Empresa foi deletada.");
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            this._connection.Close();
        }
    }
}