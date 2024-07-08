
namespace E_CommerceAPI.Errors
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int status, string? message = null)
        {
            Status = status;
            Message = message ?? GetDefaultMessage(status);
        }


        private string? GetDefaultMessage(int status)
        {
            return status switch
            {
                400 => "A bad request, you have made",
                401 => "You are not Authorized",
                404 => "Resource Not found",
                500 => "Errors are the path to the dark side. Errors leads to anger." +
                        " Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }

    }
}
