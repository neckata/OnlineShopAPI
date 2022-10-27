namespace OnlineShopServices.Service.Response
{
    /// <summary>
    /// response  base class
    /// </summary>
    public class Response
    {
        public Response()
        {
            Type = ResponseType.Fail;
        }

        public string ErrorCode { get; set; }
        public ResponseType Type { get; set; }
        public object Data { get; set; }
    }

    public enum ResponseType
    {
        InternalError = -3,
        ValidationError = -2,
        Fail = -1,
        Success = 0,
        Warning = 1,
        Info = 2,
        NoEffect = 3,
        DuplicateRecord = 4,
        RecordNotFound = 5,
    }

    public static class ErrorCode
    {
        public const string ApplicationException = "UNEXPECTED_ERROR";
        public const string RecordNotFound = "RECORD_NOT_FOUND";
        public const string MediaServiceError = "MEDIA_SERVICE_ERROR";
        public const string ObjectExceededMaxAllowedLength = "OBJECT_EXCEEDED_MAX_ALLOWED_LENGTH";
    }
}
