using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimulatedLibraryMgt.Models
{
    public class BusinessHoliday
    {
        public int BusinessHolidayId { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9]*$", ErrorMessage = "Please ensure that the first letters are in capital letter")]
        [Display(Name ="Holiday Occasion")]
        public string HolidayOccassion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
        public bool? IsActive { get; set; }

       
    }
}