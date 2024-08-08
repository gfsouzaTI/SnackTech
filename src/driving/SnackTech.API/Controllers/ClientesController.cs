using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Cliente;
using SnackTech.Domain.Ports.Driving;
using SnackTech.Domain.Ports.Driven;
using Swashbuckle.AspNetCore.Annotations;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(ILogger<ClientesController> logger, IClienteService clienteService) : CustomBaseController(logger)
    {
        private readonly IClienteService clienteService = clienteService;

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <remarks>
        /// Cria uma nova cliente no sistema.
        /// </remarks>
        /// <param name="cadastroCliente">Os dados do cliente a ser criado.</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo cliente no sistema")]
        public async Task<IActionResult> Post([FromBody] CadastroCliente cadastroCliente)
            => await CommonExecution("Clientes.Post", clienteService.Cadastrar(cadastroCliente));

        /// <summary>
        /// Obtém um cliente pelo CPF.
        /// </summary>
        /// <remarks>
        /// Retorna as informações de um cliente baseado no CPF fornecido.
        /// </remarks>
        /// <param name="cpf">O CPF do cliente a ser pesquisado.</param>
        [HttpGet]
        [Route("{cpf}")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente com o CPF informado")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
            => await CommonExecution("Clientes.GetByCpf", clienteService.IdentificarPorCpf(cpf));

        /// <summary>
        /// Obtém o cliente padrão.
        /// </summary>
        /// <remarks>
        /// Retorna as informações do cliente padrão do sistema.
        /// </remarks>
        [HttpGet]
        [Route("cliente-padrao")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente padrao")]
        public async Task<IActionResult> GetDefaultClient()
            => await CommonExecution("Clientes.GetDefaultClient", clienteService.SelecionarClientePadrao());
    }
}
