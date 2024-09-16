using System.Text.Json.Serialization;

namespace Main.Application.SystemImports.Model
{
    public class BaseErrorResponse
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("status")]
        public Main.Application.SystemImports.Model.HttpModel Status { get; set; }

        public BaseErrorResponse(string value, Main.Application.SystemImports.Model.HttpModel httpResponse)
        {
            this.Value = value;
            this.Status = httpResponse;
        }
    }
}
