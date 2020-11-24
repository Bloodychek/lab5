using System;
using System.Collections.Generic;

namespace lab5.Models
{
    public partial class Containers
    {
        public Containers()
        {
            IncomeAndExpensesOfGsm = new HashSet<IncomeAndExpensesOfGsm>();
        }

        public int ContainerId { get; set; }
        public int? Number { get; set; }
        public double? TankCapacity { get; set; }
        public int? TypeOfGsmid { get; set; }

        public virtual Gsm TypeOfGsm { get; set; }
        public virtual ICollection<IncomeAndExpensesOfGsm> IncomeAndExpensesOfGsm { get; set; }
    }
}
