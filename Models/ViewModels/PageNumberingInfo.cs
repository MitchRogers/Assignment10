using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumBowlersPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalBowlers { get; set; }
        // equation for numPages to be built based on info
        public int NumPages => (int) Math.Ceiling(((decimal)TotalBowlers / NumBowlersPerPage));
    }
}
