using System.Text.Json.Serialization;

namespace Main.Application.SystemImports.Model
{
    public class BaseAuthenticationResponse<T>
    {
        [JsonPropertyName("response")]
        public BaseCrudResponse<T> Response { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonConstructor]
        public BaseAuthenticationResponse(BaseCrudResponse<T> response, string token)
        {
            this.Response = response;
            this.Token = token;
        }
    }
}
