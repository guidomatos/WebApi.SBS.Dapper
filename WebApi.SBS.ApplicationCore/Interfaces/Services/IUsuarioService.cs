﻿using WebApi.SBS.ApplicationCore.DTO;
using WebApi.SBS.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.SBS.ApplicationCore.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<BusquedaUsuarioDto>> BuscarUsuario(FiltroBusquedaUsuarioDto param);
        Task<int> GrabarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int usuarioId);
    }
}
