using System.Net;

namespace Main.Api.SystemImports.Models
{
    public class HttpModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public HttpModel(HttpStatusCode statusCode)
        {
            this.StatusCode = (int)statusCode;
            this.StatusMessage = CreateStatusMessage(statusCode);
        }

        private string CreateStatusMessage(HttpStatusCode statusCode)
        {
            string resultMessage = string.Empty;
            int convertedStatusCode = (int)statusCode;

            switch (convertedStatusCode)
            {
                case 200:
                    resultMessage = $"{convertedStatusCode} - Solicitação bem-sucedida.";
                    break;
                case 201:
                    resultMessage = $"{convertedStatusCode} - Recurso criado com sucesso.";
                    break;
                case 202:
                    resultMessage = $"{convertedStatusCode} - Solicitação aceita para processamento, mas não concluída.";
                    break;
                case 204:
                    resultMessage = $"{convertedStatusCode} - Solicitação bem-sucedida, sem conteúdo.";
                    break;
                case 301:
                    resultMessage = $"{convertedStatusCode} - Recurso movido permanentemente.";
                    break;
                case 302:
                    resultMessage = $"{convertedStatusCode} - Recurso encontrado temporariamente em outro URI.";
                    break;
                case 304:
                    resultMessage = $"{convertedStatusCode} - Recurso não modificado.";
                    break;
                case 400:
                    resultMessage = $"{convertedStatusCode} - Solicitação inválida.";
                    break;
                case 401:
                    resultMessage = $"{convertedStatusCode} - Autenticação necessária.";
                    break;
                case 403:
                    resultMessage = $"{convertedStatusCode} - Acesso proibido.";
                    break;
                case 404:
                    resultMessage = $"{convertedStatusCode} - Recurso não encontrado.";
                    break;
                case 405:
                    resultMessage = $"{convertedStatusCode} - Método não permitido.";
                    break;
                case 409:
                    resultMessage = $"{convertedStatusCode} - Conflito no estado do recurso.";
                    break;
                case 429:
                    resultMessage = $"{convertedStatusCode} - Muitas solicitações em um curto período de tempo.";
                    break;
                case 500:
                    resultMessage = $"{convertedStatusCode} - Erro interno do servidor.";
                    break;
                case 501:
                    resultMessage = $"{convertedStatusCode} - Funcionalidade não implementada.";
                    break;
                case 502:
                    resultMessage = $"{convertedStatusCode} - Resposta inválida do gateway.";
                    break;
                case 503:
                    resultMessage = $"{convertedStatusCode} - Serviço indisponível.";
                    break;
                case 504:
                    resultMessage = $"{convertedStatusCode} - Tempo limite do gateway.";
                    break;
                default:
                    resultMessage = $"{convertedStatusCode} - Código de status desconhecido.";
                    break;
            }

            return resultMessage;
        }
    }
}
