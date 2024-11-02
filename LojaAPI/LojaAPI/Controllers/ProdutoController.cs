using LojaAPI.Models;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost("cadastrar-produto")]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
            var produtoId = await _produtoRepository.CadastrarProdutoDB(produto);
            return Ok(new { mensagem = "Produto cadastrado com sucesso!", produtoId });
        }

        [HttpGet("listar-produtos")]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _produtoRepository.ListarProdutosDB();
            return Ok(produtos);
        }

        [HttpGet("produto/{id}")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            var produto = await _produtoRepository.ObterProdutoPorIdDB(id);
            return produto != null ? Ok(produto) : NotFound(new { mensagem = "Produto não encontrado." });
        }

        [HttpPut("atualizar-produto/{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produto)
        {
            produto.Id = id;
            var atualizado = await _produtoRepository.AtualizarProdutoDB(produto);
            return atualizado > 0 ? Ok(new { mensagem = "Produto atualizado!" }) : NotFound(new { mensagem = "Produto não encontrado." });
        }

        [HttpDelete("excluir-produto/{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var excluido = await _produtoRepository.ExcluirProdutoDB(id);
            return excluido > 0 ? Ok(new { mensagem = "Produto excluído!" }) : NotFound(new { mensagem = "Produto não encontrado." });
        }
    }
}
