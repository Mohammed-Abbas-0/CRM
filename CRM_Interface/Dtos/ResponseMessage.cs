using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class ResponseMessage
    {
        public bool IsAuthenticated {  get; set; }
        public required string Message { get; set; }
    }
}
