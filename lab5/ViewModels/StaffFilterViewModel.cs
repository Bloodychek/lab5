﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.ViewModels
{
    public class StaffFilterViewModel
    {
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "StaffAge")]
        public int StaffAge { get; set; }

        [Display(Name = "StaffFunction")]
        public string StaffFunction { get; set; }

        [Display(Name = "WorkingHoursForAweek")]
        public int WorkingHoursForAweek { get; set; }
    }
}
