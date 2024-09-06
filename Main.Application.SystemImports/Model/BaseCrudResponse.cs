using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class BaseCrudResponse<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; }

        [JsonPropertyName("status")]
        public HttpModel Status { get; set; }

        [JsonConstructor]
        public BaseCrudResponse(T value, HttpModel status)
        {
            this.Value = value;
            this.Status = status;
        }
    }
}
