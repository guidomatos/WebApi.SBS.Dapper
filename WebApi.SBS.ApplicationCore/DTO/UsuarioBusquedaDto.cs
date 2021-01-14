using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.SBS.ApplicationCore.DTO
{
    public class FiltroBusquedaUsuarioDto
    {
        public int RolId { get; set; }
        public string CodigoUsuario { get; set; }
    }
    public class BusquedaUsuarioDto
    {
        public int UsuarioId { get; set; }
        public string CodigoUsuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
    }
}
