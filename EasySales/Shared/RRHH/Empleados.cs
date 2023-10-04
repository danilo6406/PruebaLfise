using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class Empleados
    {
        [Required]
        public long Id { get; set; }

        public Empresa Empresa { get; set; }

        public Sucursales Sucursal { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="Nombres deben de ser mayor a 2 caracteres")]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Compare(nameof(EmailConfirmado), ErrorMessage = "E-Mails no son iguales.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailConfirmado { get; set; }

        [Required]
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        [Required]
        public PuestoLaboral PuestoLaboral { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public Generos Genero { get; set; }

        [Required]
        public DepartamentoEmpresa DepartamentoEmpresa { get; set; }

        public string FotoPath { get; set; }

        #region Propiedades Auditoria

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoModificacion? TipoModificacion { get; set; }

        #endregion

    }
}
