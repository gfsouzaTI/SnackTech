using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Produto;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController(ILogger<ProdutosController> logger, IProdutoService produtoService) : CustomBaseController(logger)
    {
        private readonly IProdutoService produtoService = produtoService;

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <remarks>
        /// Adiciona um novo produto ao sistema com base nas informações fornecidas.
        /// </remarks>
        /// <param name="novoProduto">Os dados do novo produto a ser criado.</param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NovoProduto novoProduto)
            => await CommonExecution("Produtos.Post", produtoService.CriarNovoProduto(novoProduto));

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <remarks>
        /// Modifica os detalhes de um produto existente no sistema.
        /// </remarks>
        /// <param name="identificacao">A identificação do produto a ser atualizado.</param>
        /// <param name="produtoEditado">Os novos dados para o produto.</param>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid identificacao, [FromBody] EdicaoProduto produtoEditado)
            => await CommonExecution("Produtos.Put", produtoService.EditarProduto(identificacao, produtoEditado));

        /// <summary>
        /// Remove um produto existente.
        /// </summary>
        /// <remarks>
        /// Remove um produto do sistema com base na identificação fornecida.
        /// </remarks>
        /// <param name="identificacao">A identificação do produto a ser removido.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        public async Task<IActionResult> Delete([FromRoute] string identificacao)
            => await CommonExecution("Produtos.Delete", produtoService.RemoverProduto(identificacao));

        /// <summary>
        /// Obtém produtos por categoria.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de produtos que pertencem à categoria especificada.
        /// </remarks>
        /// <param name="categoriaId">O ID da categoria para buscar os produtos.</param>
        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoProduto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{categoriaId:int}")]
        public async Task<IActionResult> GetByCategory(int categoriaId)
            => await CommonExecution("Produtos.GetPorCategoria", produtoService.BuscarPorCategoria(categoriaId));
    }
}
