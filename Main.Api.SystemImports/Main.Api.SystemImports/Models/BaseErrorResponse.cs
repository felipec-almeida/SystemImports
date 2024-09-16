namespace Main.Api.SystemImports.Models
{
    public class BaseErrorResponse
    {
        public string Value { get; set; }
        public Main.Api.SystemImports.Models.HttpModel Status { get; set; }

        public BaseErrorResponse(string value, Main.Api.SystemImports.Models.HttpModel httpResponse)
        {
            this.Value = value;
            this.Status = httpResponse;
        }
    }
}
