using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Queries.Handler
{
    public class GetClientsCommandHandler : IRequestHandler<GetClientsCommand, List<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientsCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientDto>> Handle(GetClientsCommand request, CancellationToken cancellationToken)
        {
            var clientDtos = await _clientRepository.GetClientsAsync(request.CompanyId);
            return clientDtos;
        }
    }
}
