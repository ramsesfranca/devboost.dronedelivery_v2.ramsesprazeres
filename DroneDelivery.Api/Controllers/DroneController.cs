using DroneDelivery.Application.Commands.Drones;
using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using DroneDelivery.Application.Queries;
using MediatR;
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
        private readonly IMediator _mediator;

        public DroneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DroneModel>>> Obter()
        {
            var response = await _mediator.Send(new ListarDronesQuery());
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
        }

        [HttpGet("situacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DroneSituacaoModel>>> ListarDrones()
        {
            var response = await _mediator.Send(new ListarSituacaoDronesQuery());
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
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
        /// <param name="command"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Adicionar(CriarDroneCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok();
        }

        //[HttpPost("{id}/pedidos")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> LiberarDrones(Guid id)
        //{
        //    try
        //    {
        //        await _droneService.AtualizarPedidosEntregues(id);

        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500);
        //    }
        //}

    }
}
