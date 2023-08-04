namespace EcommerceBackend.Contracts.Formats
{
    public class MessageResponseObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }

        public MessageResponseObject(bool succes, string message, Object result ) {
            Success = succes;
            Message = message;
            Result = result;
        }
    }

    public class MessageResponseList<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }

        public MessageResponseList(bool succes, string message, List<T> result)
        {
            Success = succes;
            Message = message;
            Data = result;
        }
    }
}
