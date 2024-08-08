using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Pedido;
using SnackTech.Domain.Ports.Driving;
using SnackTech.Domain.Ports.Driven;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(ILogger<PedidosController> logger, IPedidoService pedidoService) : CustomBaseController(logger)
    {
        private readonly IPedidoService pedidoService = pedidoService;

        /// <summary>
        /// Inicia um novo pedido para o cliente.
        /// </summary>
        /// <remarks>
        /// Cria um novo pedido para o cliente especificado pelo CPF.
        /// </remarks>
        /// <param name="cpfCliente">O CPF do cliente para iniciar o pedido.</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("iniciar")]
        [SwaggerOperation(Summary = "Inicia um novo pedido a partir do CPF de um cliente previamente cadastrado no banco. Informe null para iniciar um novo pedido do cliente padrÃ£o")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o identificador do novo pedido", typeof(Guid))]
        public async Task<IActionResult> IniciarPedido([FromBody] string cpfCliente)
            => await CommonExecution("Pedidos.IniciarPedido", pedidoService.IniciarPedido(cpfCliente));

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        /// <remarks>
        /// Atualiza os detalhes de um pedido existente no sistema.
        /// </remarks>
        /// <param name="atualizacaoPedido">Os dados de atualização do pedido.</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("atualizar")]
        [SwaggerOperation(Summary = "Atualiza o pedido (itens anexos) que ainda nÃ£o esteja no status aguardando pagamento")]
        public async Task<IActionResult> AtualizarPedido([FromBody] AtualizacaoPedido atualizacaoPedido)
            => await CommonExecution("Pedidos.AtualizarPedido", pedidoService.AtualizarPedido(atualizacaoPedido));

        /// <summary>
        /// Finaliza um pedido para pagamento.
        /// </summary>
        /// <remarks>
        /// Finaliza um pedido específico para que esteja pronto para pagamento.
        /// </remarks>
        /// <param name="identificacao">A identificação(ID) do pedido a ser finalizado.</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("finalizar-para-pagamento")]
        [SwaggerOperation(Summary = "Finaliza o pedido com o identificador informado e o coloca na situaÃ§Ã£o de aguardando pagamento")]
        public async Task<IActionResult> FinalizarPedidoParaPagamento([FromBody] string identificacao)
            => await CommonExecution("Pedidos.FinalizarPedidoParaPagamento", pedidoService.FinalizarPedidoParaPagamento(identificacao));

        /// <summary>
        /// Lista todos os pedidos que estão aguardando pagamento.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de pedidos que estão atualmente aguardando pagamento.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoPedido>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("aguardando-pagamento")]
        [SwaggerOperation(Summary = "Retorna a lista de pedidos com status aguardando pagamento")]
        public async Task<IActionResult> ListarPedidosParaPagamento()
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.ListarPedidosParaPagamento());

        /// <summary>
        /// Busca um pedido pela identificação.
        /// </summary>
        /// <remarks>
        /// Retorna os detalhes de um pedido específico baseado na identificação fornecida.
        /// </remarks>
        /// <param name="identificacao">A identificação do pedido a ser buscado.</param>
        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Retorna o pedido com o identificador informado")]
        public async Task<IActionResult> BuscarPorIdenticacao([FromRoute] string identificacao)
            => await CommonExecution("Pedidos.BuscarPorIdenticacao", pedidoService.BuscarPorIdenticacao(identificacao));

        /// <summary>
        /// Busca o último pedido do cliente.
        /// </summary>
        /// <remarks>
        /// Retorna os detalhes do último pedido feito pelo cliente especificado pelo CPF.
        /// </remarks>
        /// <param name="cpfCliente">O CPF do cliente para buscar o último pedido.</param>
        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("ultimo-pedido-cliente")]
        [SwaggerOperation(Summary = "Retorna o pedido mais recente do cliente com CPF informado. NÃ£o Ã© permitida a consulta do Ãºltimo pedido do cliente padrÃ£o")]
        public async Task<IActionResult> BuscarUltimoPedidoCliente([FromQuery] string cpfCliente)
            => await CommonExecution("Pedidos.BuscarUltimoPedidoCliente", pedidoService.BuscarUltimoPedidoCliente(cpfCliente));
    }
}
