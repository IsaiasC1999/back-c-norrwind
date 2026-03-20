namespace ef_nortwith.DTOs;

public class PaginationMeta
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
}
