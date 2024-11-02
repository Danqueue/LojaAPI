using LojaAPI.Models;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoRepository _carrinhoRepository;

        public CarrinhoController(CarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItem([FromBody] Carrinho item)
        {
            var itemId = await _carrinhoRepository.AdicionarItemCarrinhoDB(item);
            return Ok(new { mensagem = "Item adicionado ao carrinho!", itemId });
        }

        [HttpGet("listar-itens/{usuarioId}")]
        public async Task<IActionResult> ListarItens(int usuarioId)
        {
            var itens = await _carrinhoRepository.ListarItensCarrinhoDB(usuarioId);
            return Ok(itens);
        }

        [HttpDelete("remover-item/{id}")]
        public async Task<IActionResult> RemoverItem(int id)
        {
            var removido = await _carrinhoRepository.RemoverItemCarrinhoDB(id);
            return removido > 0 ? Ok(new { mensagem = "Item removido do carrinho!" }) : NotFound(new { mensagem = "Item não encontrado no carrinho." });
        }
    }
}
