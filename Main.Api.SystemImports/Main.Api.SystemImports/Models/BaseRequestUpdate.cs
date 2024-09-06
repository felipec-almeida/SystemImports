namespace Main.Api.SystemImports.Models
{
    public class BaseRequestUpdate<T, T2>
    {
        public T Data { get; set; }
        public T2 NewValue { get; set; }

        public BaseRequestUpdate(T data, T2 newValue)
        {
            Data = data;
            NewValue = newValue;
        }
    }
}
