using WebApi.SBS.ApplicationCore.DTO;
using WebApi.SBS.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.SBS.ApplicationCore.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<BusquedaUsuarioDto>> BuscarUsuario(FiltroBusquedaUsuarioDto param);
        Task<int> InsertarUsuario(Usuario usuario);
        Task<int> ModificarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int usuarioId);
    }
}
