using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly IDroneService _droneService;

        public DroneController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [HttpGet]
        public async Task<IEnumerable<DroneModel>> Obter()
        {
            return await _droneService.ObterAsync();
        }

        [HttpGet("situacao")]
        public async Task<IEnumerable<DroneSituacaoModel>> ListarDrones()
        {
            return await _droneService.ListarDronesAsync();
        }

        [HttpGet("situacao/{id}")]
        public async Task<DroneSituacaoModel> ListarDrones(Guid id)
        {
            return await _droneService.ListarDroneAsync(id);
        }

        /// <summary>
        /// Criar um drone
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/drone
        ///     {
        ///         "capacidade": 12000,
        ///         "velocidade": 3.33333,
        ///         "autonomia": 35,
        ///         "carga": 60
        ///     }
        ///
        /// </remarks>        
        /// <param name="createDroneModel"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Adicionar(CreateDroneModel createDroneModel)
        {
            try
            {
                await _droneService.AdicionarAsync(createDroneModel);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{id}/pedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LiberarDrones(Guid id)
        {
            try
            {
                await _droneService.AtualizarPedidosEntregues(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
