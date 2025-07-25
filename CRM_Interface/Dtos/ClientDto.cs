using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public record ClientDto( string UserId, string UserName,string Address,string PhoneNumber, int CamapignCount, List<string> CamapignName  );
}
