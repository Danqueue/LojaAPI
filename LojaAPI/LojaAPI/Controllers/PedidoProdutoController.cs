using LojaAPI.Models;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoProdutoController : ControllerBase
    {
        private readonly PedidoProdutoRepository _pedidoProdutoRepository;

        public PedidoProdutoController(PedidoProdutoRepository pedidoProdutoRepository)
        {
            _pedidoProdutoRepository = pedidoProdutoRepository;
        }

        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> ListarPedidoProdutos(int pedidoId)
        {
            var pedidoProdutos = await _pedidoProdutoRepository.ListarPedidoProdutosPorPedidoId(pedidoId);
            return Ok(pedidoProdutos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPedidoProduto([FromBody] PedidoProduto pedidoProduto)
        {
            var id = await _pedidoProdutoRepository.AdicionarPedidoProduto(pedidoProduto);
            return Ok(new { mensagem = "Produto adicionado ao pedido com sucesso", id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedidoProduto(int id, [FromBody] PedidoProduto pedidoProduto)
        {
            pedidoProduto.Id = id;
            await _pedidoProdutoRepository.AtualizarPedidoProduto(pedidoProduto);
            return Ok(new { mensagem = "Produto do pedido atualizado com sucesso" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverPedidoProduto(int id)
        {
            await _pedidoProdutoRepository.RemoverPedidoProduto(id);
            return Ok(new { mensagem = "Produto do pedido removido com sucesso" });
        }
    }
}
