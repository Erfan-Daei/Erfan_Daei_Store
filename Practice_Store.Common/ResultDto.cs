namespace Practice_Store.Common
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object StatusCode { get; set; }
    }

    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public object StatusCode { get; set; }
    }
}
