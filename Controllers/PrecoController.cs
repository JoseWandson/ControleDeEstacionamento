using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Servicos;
using ControleDeEstacionamento.Servico.ViewModelExtensions;
using ControleDeEstacionamento.Transporte.Response;
using ControleDeEstacionamento.Transporte.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeEstacionamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecoController : Controller
    {
        private readonly IPrecoServico _precoServico;

        public PrecoController(IPrecoServico precoServico)
        {
            _precoServico = precoServico;
        }

        // GET preco
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_precoServico.ObterTodos().Select(p => p.TransformarModelEmView()));
        }

        // GET preco/1
        [HttpGet("{id:long}")]
        public IActionResult ObterPorId(long id)
        {
            Preco preco = _precoServico.ObterPorId(id);

            if (preco == null)
            {
                return NotFound();
            }

            return Ok(preco.TransformarModelEmView());
        }

        // POST preco
        [HttpPost]
        public IActionResult Salvar([FromBody]PrecoViewModel viewModel)
        {
            return Ok(new ValorResponse<long>(_precoServico.Salvar(viewModel)));
        }

        // DELETE preco/5
        [HttpDelete("{id}")]
        public IActionResult Excluir(long id)
        {
            return Ok(new ValorResponse<long>(_precoServico.Excluir(id)));
        }
    }
}
