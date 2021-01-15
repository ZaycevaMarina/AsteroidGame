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
        }
        /// <summary>
        /// Cреднемесячная заработная плата
        /// </summary>
        /// <returns></returns>
        public override double CalculateAverageMonthlySalary()
        {
            return FixedPayment;
        }
    }
}
