using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.SBS.ApplicationCore.DTO;
using WebApi.SBS.ApplicationCore.Entities;
using WebApi.SBS.ApplicationCore.Interfaces.Services;

namespace WebApi.SGS.Controllers
{
    /// <summary>
    /// UsuarioController
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<Controller> _logger;

        /// <summary>
        /// UsuarioController
        /// </summary>
        /// <param name="usuarioService"></param>
        /// <param name="logger"></param>
        public UsuarioController(IUsuarioService usuarioService, ILogger<Controller> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }


        /// <summary>
        /// BuscarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Buscar")]
        public async Task<IActionResult> BuscarUsuario([FromBody] FiltroBusquedaUsuarioDto param)
        {
            try
            {
                var result = await _usuarioService.BuscarUsuario(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// GrabarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Grabar")]
        public async Task<IActionResult> GrabarUsuario([FromBody] Usuario param)
        {
            try
            {
                var result = await _usuarioService.GrabarUsuario(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// EliminarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Eliminar")]
        public async Task<IActionResult> EliminarUsuario([FromBody] Usuario param)
        {
            try
            {
                var result = await _usuarioService.EliminarUsuario(param.UsuarioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                return BadRequest();
            }
        }

    }
}
