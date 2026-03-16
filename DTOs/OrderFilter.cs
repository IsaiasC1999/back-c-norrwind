namespace ef_nortwith.DTOs;

public class OrderFilter
{
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Cliente { get; set; }
    public string? Empleado { get; set; }
    public int? EmpleadoId { get; set; }
}
