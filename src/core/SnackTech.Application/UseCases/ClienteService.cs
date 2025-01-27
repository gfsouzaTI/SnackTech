using Microsoft.Extensions.Logging;
using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Cliente;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;
using SnackTech.Domain.Ports.Driving;

namespace SnackTech.Application.UseCases
{
    public class ClienteService(ILogger<ClienteService> logger, IClienteRepository clienteRepository) : BaseService(logger),IClienteService
    {
        private readonly IClienteRepository clienteRepository = clienteRepository;

        public async Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente)
        {
            async Task<Result<RetornoCliente>> processo(){
                var novoCliente = new Cliente(cadastroCliente.Nome,cadastroCliente.Email,cadastroCliente.CPF);
                await clienteRepository.InserirClienteAsync(novoCliente);
                var retorno = RetornoCliente.APartirDeCliente(novoCliente);
                return new Result<RetornoCliente>(retorno);
            }
            return await CommonExecution("ClienteService.Cadastrar",processo);
        }

        public async Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf)
        {
            async Task<Result<RetornoCliente>> processo(){
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                var cliente = await clienteRepository.PesquisarPorCpfAsync(cpf);

                if(cliente == null){
                    return new Result<RetornoCliente>($"{cpf} não encontrado.",true);
                }

                var retorno = RetornoCliente.APartirDeCliente(cliente);
                return new Result<RetornoCliente>(retorno);
            }
            return await CommonExecution($"ClienteService.IdentificarPorCpf - {cpf}",processo);
        }

        public async Task<Result<Guid>> SelecionarClientePadrao()
        {
            async Task<Result<Guid>> processo(){
                var clientePadrao = await clienteRepository.PesquisarClientePadraoAsync();
                var retorno = clientePadrao.Id;
                return new Result<Guid>(retorno);
            }
            return await CommonExecution("ClienteService.SelecionarClientePadrao",processo);
        }
    }
}