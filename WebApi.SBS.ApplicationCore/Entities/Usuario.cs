using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.SBS.ApplicationCore.Entities
{
    public class Usuario
    {
        [SwaggerSchema("El identificador del usuario", ReadOnly = true)]
        public int UsuarioId { get; set; }
        [SwaggerSchema("El identificador de rol del usuario")]
        public int RolId { get; set; }
        [SwaggerSchema("El código del usuario")]
        public string CodigoUsuario { get; set; }
        [SwaggerSchema("La clase del usuario")]
        public string ClaveSecreta { get; set; }
        [SwaggerSchema("El email del usuario")]
        public string Email { get; set; }
        [SwaggerSchema("El apellido paterno del usuario")]
        public string ApellidoPaterno { get; set; }
        [SwaggerSchema("El apellido materno del usuario")]
        public string ApellidoMaterno { get; set; }
        [SwaggerSchema("El primer nombre del usuario")]
        public string PrimerNombre { get; set; }
        [SwaggerSchema("El segundo nombre del usuario")]
        public string SegundoNombre { get; set; }
        [SwaggerSchema("El alias del usuario")]
        public string Alias { get; set; }
        [SwaggerSchema("Indicador de si es primera vez que se logueará el usuario")]
        public int PrimeraVezLogin { get; set; }
        [SwaggerSchema("Indicador si el usuario está activo")]
        public int Activo { get; set; }
    }
}
