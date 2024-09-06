using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class BaseResponseList<T>
    {
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }

        [JsonPropertyName("totalLines")]
        public long TotalLines { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}
