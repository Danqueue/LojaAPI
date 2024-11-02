using LojaAPI.Models;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpPost("registrar-pedido")]
        public async Task<IActionResult> RegistrarPedido([FromBody] Pedidos pedido)
        {
            pedido.DataPedido = DateTime.Now;
            var pedidoId = await _pedidoRepository.RegistrarPedidoDB(pedido);
            return Ok(new { mensagem = "Pedido registrado com sucesso!", pedidoId });
        }

        [HttpGet("listar-pedidos/{usuarioId}")]
        public async Task<IActionResult> ListarPedidos(int usuarioId)
        {
            var pedidos = await _pedidoRepository.ListarPedidosDB(usuarioId);
            return Ok(pedidos);
        }

        [HttpPut("atualizar-status/{pedidoId}")]
        public async Task<IActionResult> AtualizarStatus(int pedidoId, [FromBody] string novoStatus)
        {
            var atualizado = await _pedidoRepository.AtualizarStatusPedidoDB(pedidoId, novoStatus);
            return atualizado > 0 ? Ok(new { mensagem = "Status do pedido atualizado!" }) : NotFound(new { mensagem = "Pedido não encontrado." });
        }
    }
}
