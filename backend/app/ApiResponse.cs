namespace Early_warning
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageByStatusCode(StatusCode);
        }

        private string? GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request, You Have Made",
                401 => "Authorized, You are Not",
                404 => "Resourse Was Not Found",
                500 => "Errors Are The Path To The Dark Side. Errors Lead To Anger. Anger Lead to Hate. Hate Lead to Career Change.",
                _ => null,
            };
        }
    }
}
