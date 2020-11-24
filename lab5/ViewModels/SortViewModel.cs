using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.ViewModels
{
    public enum SortState
    {
        //Gsm
        TypeOfGsmAsc,
        TypeOfGsmDesc,
        TTkofTypeAsc,
        TTkofTypeDesc,
        //Staff
        FullNameAsc,
        FullNameDesc,
        StaffAgeAsc,
        StaffAgeDesc,
        StaffFunctionAsc,
        StaffFunctionDesc,
        WorkingHoursForAweekAsc,
        WorkingHoursForAweekDesc,
        // IncomeAndExpensesOfGsm
        NumberOfCapacityAsc,
        NumberOfCapacityDesc,
        IncomeOrExpensePerliterAsc,
        IncomeOrExpensePerliterDesc,
        DateAndTimeOfTheOperationIncomeOrExpenseAsc,
        DateAndTimeOfTheOperationIncomeOrExpenseDesc,
        ResponsibleForTheOperationAsc,
        ResponsibleForTheOperationDesc
    }

    public class SortViewModel
    {
        //GSM
        public SortState TypeOfGsmSort { get; set; }
        public SortState TTkofTypeSort { get; set; }

        //Staff
        public SortState FullNameSort { get; set; }
        public SortState StaffAgeSort { get; set; }
        public SortState StaffFunctionSort { get; set; }
        public SortState WorkingHoursForAweekSort { get; set; }


        //IncomeAndExpensesOfGsm
        public SortState NumberOfCapacitySort { get; set; }
        public SortState IncomeOrExpensePerliterSort { get; set; }
        public SortState DateAndTimeOfTheOperationIncomeOrExpenseSort { get; set; }
        public SortState ResponsibleForTheOperationSort { get; set; }


        public SortState CurrentState { get; set; }
        public SortViewModel(SortState state)
        {
            //Gsm
            TypeOfGsmSort = state == SortState.TypeOfGsmAsc ? SortState.TypeOfGsmDesc : SortState.TypeOfGsmAsc;
            TTkofTypeSort = state == SortState.TTkofTypeAsc ? SortState.TTkofTypeDesc : SortState.TTkofTypeAsc;
            //Shows
            FullNameSort = state == SortState.FullNameAsc ? SortState.FullNameDesc : SortState.FullNameAsc;
            StaffAgeSort = state == SortState.StaffAgeAsc ? SortState.StaffAgeDesc : SortState.StaffAgeAsc;
            StaffFunctionSort = state == SortState.StaffFunctionAsc ? SortState.StaffFunctionDesc : SortState.StaffFunctionAsc;
            WorkingHoursForAweekSort = state == SortState.WorkingHoursForAweekAsc ? SortState.WorkingHoursForAweekDesc : SortState.WorkingHoursForAweekAsc;

            //IncomeAndExpensesOfGsm
            NumberOfCapacitySort = state == SortState.NumberOfCapacityAsc ? SortState.NumberOfCapacityDesc : SortState.NumberOfCapacityAsc;
            IncomeOrExpensePerliterSort = state == SortState.IncomeOrExpensePerliterAsc ? SortState.IncomeOrExpensePerliterDesc : SortState.IncomeOrExpensePerliterAsc;
            DateAndTimeOfTheOperationIncomeOrExpenseSort = state == SortState.DateAndTimeOfTheOperationIncomeOrExpenseAsc ? SortState.DateAndTimeOfTheOperationIncomeOrExpenseDesc : SortState.DateAndTimeOfTheOperationIncomeOrExpenseAsc;
            ResponsibleForTheOperationSort = state == SortState.ResponsibleForTheOperationAsc ? SortState.ResponsibleForTheOperationDesc : SortState.ResponsibleForTheOperationAsc;

            CurrentState = state;
        }
    }
}
