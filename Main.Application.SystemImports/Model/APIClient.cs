using Main.Application.SystemImports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class APIClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> SendRequestAsync(
    ERequestType methodType,
    string url,
    object body = null,
    Dictionary<string, string> queryParams = null)
        {
            try
            {
                // Adicionar parâmetros de consulta à URL, se houver
                if (queryParams != null && queryParams.Count > 0)
                {
                    var uriBuilder = new UriBuilder(url);
                    var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                    foreach (var param in queryParams)
                    {
                        query[param.Key] = param.Value;
                    }

                    uriBuilder.Query = query.ToString();
                    url = uriBuilder.ToString();
                }

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);

                switch (methodType)
                {
                    case ERequestType.GET:
                        request.Method = HttpMethod.Get;
                        break;

                    case ERequestType.POST:
                        request.Method = HttpMethod.Post;
                        if (body != null)
                        {
                            var postContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
                            request.Content = postContent;
                        }
                        break;

                    case ERequestType.PUT:
                        request.Method = HttpMethod.Put;
                        if (body != null)
                        {
                            var putContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
                            request.Content = putContent;
                        }
                        break;

                    case ERequestType.DELETE:
                        request.Method = HttpMethod.Delete;
                        break;

                    default:
                        throw new NotSupportedException($"Método HTTP {methodType} não suportado.");
                }

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
                return null;
            }
        }

    }
}
