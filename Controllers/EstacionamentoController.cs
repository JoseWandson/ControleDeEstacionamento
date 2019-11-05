using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Servicos;
using ControleDeEstacionamento.Servico.ViewModelExtensions;
using ControleDeEstacionamento.Transporte.Requests;
using ControleDeEstacionamento.Transporte.Response;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeEstacionamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstacionamentoController : Controller
    {
        private readonly IEstacionamentoServico _estacionamentoServico;

        public EstacionamentoController(IEstacionamentoServico estacionamentoServico)
        {
            _estacionamentoServico = estacionamentoServico;
        }

        // GET estacionamento
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_estacionamentoServico.ObterTodos().Select(e => e.TransformarModelEmView()));
        }

        // GET estacionamento/1
        [HttpGet("{id:long}")]
        public IActionResult ObterPorId(long id)
        {
            Estacionamento estacionamento = _estacionamentoServico.ObterPorId(id);

            if (estacionamento == null)
            {
                return NotFound();
            }

            return Ok(estacionamento.TransformarModelEmView());
        }

        // POST estacionamento/Entrada
        [HttpPost("entrada")]
        public IActionResult Entrada([FromBody]EstacionamentoEntradaRequest viewModel)
        {
            return Ok(new ValorResponse<long>(_estacionamentoServico.Entrada(viewModel)));
        }

        // POST estacionamento/Saida
        [HttpPost("saida")]
        public IActionResult Saida([FromBody]EstacionamentoSaidaRequest viewModel)
        {
            return Ok(new ValorResponse<long>(_estacionamentoServico.Saida(viewModel)));
        }
    }
}
