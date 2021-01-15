using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2
{
    /// <summary>
    /// Работник  с  почасовой  оплатой
    /// </summary>
    class EmployeeHourlyPayment :Employee
    {
        /// <summary>
        /// Почасовая ставка
        /// </summary>
        private double HourlyRate;
        public EmployeeHourlyPayment(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }
        /// <summary>
        /// Cреднемесячная заработная плата
        /// </summary>
        /// <returns></returns>
        public override double CalculateAverageMonthlySalary()
        {
            return 20.8 * 8 * HourlyRate;
        }

        public override string ToString()
        {
            return $"Работник  с  почасовой  оплатой {HourlyRate:F2}, среднемесячная заработная плата {CalculateAverageMonthlySalary():F2}";
        }
    }
}
