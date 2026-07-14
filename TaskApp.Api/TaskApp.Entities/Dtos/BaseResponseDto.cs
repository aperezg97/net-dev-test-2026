namespace TaskApp.Entities.Dtos
{
    public class BaseResponseDto<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
