﻿using lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.ViewModels
{
    public class GsmViewModel
    {
        public IEnumerable<Gsm> Gsms { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public Gsm Gsm { get; set; }
        public DeleteViewModels DeleteViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public GsmFilterViewModel GsmFilterViewModel { get; set; }
    }
}
