using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.SBS.ApplicationCore.DTO;
using WebApi.SBS.ApplicationCore.Entities;
using WebApi.SBS.ApplicationCore.Interfaces.Services;

namespace WebApi.SGS.Controllers
{
    /// <summary>
    /// UsuarioController
    /// </summary>
    [Produces("application/json")]

    [ApiController]
    [ApiVersion("1")]
    //[ApiVersion("2")]
    [Route("vv{version:apiVersion}/[controller]")]
    [SwaggerTag("Crea, lee, actualiza y elimina Usuarios")]
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
        [SwaggerResponse(201, "El usuario fue creado", typeof(BusquedaUsuarioDto))]
        [SwaggerResponse(400, null, typeof(ErrorResponse))]
        //[SwaggerResponse(500)]
        [SwaggerOperation(
            Summary = "Buscar un usuario",
            Description = "Buscar un usuario ingresando parámetros",
            OperationId = "BuscarUsuario",
            Tags = new[] { "Usuario" }
        )]
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
                return BadRequest(new ErrorResponse { ErrorCode = 404, ErrorMessage = "Usuario no encontrado" });

            }
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="object">Usser</param>
        /// <returns></returns>
        [HttpPost("Grabar")]
        [SwaggerResponse(201, "El usuario fue creado", typeof(Usuario))]
        [SwaggerResponse(400, null, typeof(ErrorResponse))]
        //[SwaggerResponse(500)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Grabar un usuario",
            Description = "Grabar un usuario ingresando parámetros",
            OperationId = "GrabarUsuario",
            Tags = new[] { "Usuario" }
        )]
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
                return BadRequest(new ErrorResponse { ErrorCode = 404, ErrorMessage = "No se pudo grabar" });
            }
        }

        /// <summary>
        /// Eliminar un usuario específico
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        //[HttpPost("Eliminar")]
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "El usuario fue eliminado")]
        [SwaggerResponse(400, null, typeof(ErrorResponse))]
        //[SwaggerResponse(500)]
        [SwaggerOperation(
            Summary = "Eliminar un usuario",
            Description = "Eliminar un usuario ingresando el id",
            OperationId = "EliminarUsuario",
            Tags = new[] { "Usuario" }
        )]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var result = await _usuarioService.EliminarUsuario(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
                return BadRequest(new ErrorResponse { ErrorCode = 404, ErrorMessage = "No se pudo eliminar" });
            }
        }

    }
}
