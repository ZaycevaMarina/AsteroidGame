using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2
{
    /// <summary>
    /// Работник  с  фиксированной  оплатой
    /// </summary>
    class EmployeeFixedPayment:Employee
    {
        /// <summary>
        /// Фиксированная месячная оплата
        /// </summary>
        private double FixedPayment;
        public EmployeeFixedPayment(double fixedPayment)
        {
            FixedPayment = fixedPayment;
            AverageMonthlySalary = CalculateAverageMonthlySalary();
        }
        /// <summary>
        /// Cреднемесячная заработная плата
        /// </summary>
        /// <returns></returns>
        public override double CalculateAverageMonthlySalary()
        {
            return FixedPayment;
        }

        public override string ToString()
        {
            return "Работник  с  фиксиров.  оплатой " + String.Format("{0," + __Format + "}", FixedPayment)
                    + " среднемесячная заработная плата - " + String.Format("{0," + __Format + "}", CalculateAverageMonthlySalary());
        }
        public override string ToString(string format)
        {
            return "Работник  с  фиксиров.  оплатой " + String.Format("{0," + format + "}", FixedPayment)
                        + " среднемесячная заработная плата - " + String.Format("{0," + format + "}", CalculateAverageMonthlySalary());
        }
    }
}
