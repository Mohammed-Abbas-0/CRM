using CRM_Interface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientDto>> GetClientsAsync(string companyId);
    }
}
