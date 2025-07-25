using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class MonthlyCustomerStatsDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }

        public string MonthName => CultureInfo.GetCultureInfo("ar-EG")
                                       .DateTimeFormat.GetMonthName(Month);
    }

    
}
