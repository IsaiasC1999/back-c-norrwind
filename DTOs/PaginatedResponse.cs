namespace ef_nortwith.DTOs;

public class PaginatedResponse<T>
{
    public bool Success { get; set; }
    public List<T>? Result { get; set; }
    public PaginationMeta? Pagination { get; set; }
    public string Error { get; set; } = "";
}
