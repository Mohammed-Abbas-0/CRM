using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class AddRoleDto
    {
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
    }
}
