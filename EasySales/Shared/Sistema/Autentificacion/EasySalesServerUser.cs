using Microsoft.AspNetCore.Identity;

namespace EasySales.Shared;

public class EasySalesServerUser : IdentityUser
{

    public string NombreCompleto { get; set; }

    public string NumeroIdentificacion { get; set; }

    public int EmpresaId { get; set; }

    public int SucursalId { get; set; }

    public bool Activo { get; set; }

    public string UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int? TipoModificacionId { get; set; }

}
