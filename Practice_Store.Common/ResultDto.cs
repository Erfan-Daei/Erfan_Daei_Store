namespace Practice_Store.Common
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Status_Code Status_Code { get; set; }
        public object StatusCode { get; set; }
    }

    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Status_Code Status_Code { get; set; }
        public T? Data { get; set; }
        public object StatusCode { get; set; }
    }
    public enum Status_Code
    {
        // Successful responses
        OK = 200, // Standard response for successful HTTP requests.
        CREATED = 201, // The request has been fulfilled, resulting in the creation of a new resource.
        ACCEPTED = 202, // The request has been accepted for processing, but the processing has not been completed.
        NO_CONTENT = 204, // The server successfully processed the request and is not returning any content.

        // Client error responses
        BAD_REQUEST = 400, // The server could not understand the request due to invalid syntax.
        UNAUTHORIZED = 401, // The client must authenticate itself to get the requested response.
        FORBIDDEN = 403, // The client does not have access rights to the content.
        NOT_FOUND = 404, // The server can not find the requested resource.

        // Server error responses
        INTERNAL_SERVER_ERROR = 500, // The server has encountered a situation it doesn't know how to handle.
        NOT_IMPLEMENTED = 501, // The request method is not supported by the server and cannot be handled.
        BAD_GATEWAY = 502, // The server, while acting as a gateway, received an invalid response from the upstream server.
        SERVICE_UNAVAILABLE = 503 // The server is not ready to handle the request. Common causes are a server being down for maintenance or overloaded.
    }
}
