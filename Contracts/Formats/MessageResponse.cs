namespace SistemaEncomienda.Contracts.Formats
{
    public class MessageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }

        public MessageResponse(bool succes, string message, Object result ) {
            Success = succes;
            Message = message;
            Result = result;
        }
    }
}
