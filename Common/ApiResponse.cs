namespace Tank_Wiki.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public static ApiResponse<T> successResponse(
                                               T? data,
                                               string message = "Opration executed successfully")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data

        };
    }
}
