namespace Main.Api.SystemImports.Models
{

    public class BaseCrudResponse<T>
    {
        public T Value { get; set; }
        public Main.Api.SystemImports.Models.HttpModel Status { get; set; }

        public BaseCrudResponse(T value, Main.Api.SystemImports.Models.HttpModel httpResponse)
        {
            this.Value = value;
            this.Status = httpResponse;
        }
    }
}
