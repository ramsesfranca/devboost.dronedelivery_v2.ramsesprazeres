using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoModel>> Obter()
        {
            return await _pedidoService.ObterAsync();
        }


        /// <summary>
        /// Criar um pedido
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/pedido
        ///     {
        ///        "peso": 10,
        ///        "latitude": -23.5753639,
        ///        "longitude": -46.692417
        ///     }
        ///
        /// </remarks>        
        /// <param name="createPedidoModel"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Adicionar(CreatePedidoModel createPedidoModel)
        {

            try
            {
                var response = await _pedidoService.AdicionarAsync(createPedidoModel);

                if (!response)
                    return BadRequest();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
