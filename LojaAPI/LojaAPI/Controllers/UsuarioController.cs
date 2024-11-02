using LojaAPI.Models;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            var usuarioId = await _usuarioRepository.CadastrarUsuarioDB(usuario);
            return Ok(new { mensagem = "Usuário cadastrado com sucesso!", usuarioId });
        }

        [HttpGet("listar-usuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioRepository.ListarUsuariosDB();
            return Ok(usuarios);
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorIdDB(id);
            return usuario != null ? Ok(usuario) : NotFound(new { mensagem = "Usuário não encontrado." });
        }

        [HttpPut("atualizar-usuario/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            usuario.Id = id;
            var atualizado = await _usuarioRepository.AtualizarUsuarioDB(usuario);
            return atualizado > 0 ? Ok(new { mensagem = "Usuário atualizado!" }) : NotFound(new { mensagem = "Usuário não encontrado." });
        }

        [HttpDelete("excluir-usuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var excluido = await _usuarioRepository.ExcluirUsuarioDB(id);
            return excluido > 0 ? Ok(new { mensagem = "Usuário excluído!" }) : NotFound(new { mensagem = "Usuário não encontrado." });
        }
    }
}
