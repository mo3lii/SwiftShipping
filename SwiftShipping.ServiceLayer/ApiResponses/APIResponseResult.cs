namespace SwiftShipping.ServiceLayer.ApiResponses
{
    public class APIResponseResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public APIResponseResult(T data, bool success = true, string msg = null)
        {
            Success = success;
            Message = msg;
            Data = data;
        }
    }
}
