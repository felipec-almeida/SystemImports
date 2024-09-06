using System.Collections.Generic;

namespace Main.Api.SystemImports.Models
{
    public class BaseResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long TotalLines { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public BaseResponse(IEnumerable<T> data, long totalLines, int currentPage, int pageSize)
        {
            Data = data;
            TotalLines = totalLines;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
