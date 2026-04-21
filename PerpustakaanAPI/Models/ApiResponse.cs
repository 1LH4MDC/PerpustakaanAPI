namespace PerpustakaanAPI.Models
{
    // Response untuk sukses dengan data
    public class ApiResponse<T>
    {
        public string status { get; set; } = "success";
        public T? data { get; set; }
        public Meta? meta { get; set; }
    }

    // Response untuk error
    public class ApiError
    {
        public string status { get; set; } = "error";
        public string message { get; set; } = "";
    }

    // Meta untuk list (pagination info)
    public class Meta
    {
        public int total { get; set; }
    }
}