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
        private readonly double HourlyRate;
        public EmployeeHourlyPayment(double hourlyRate)
        {
            HourlyRate = hourlyRate;
            AverageMonthlySalary = CalculateAverageMonthlySalary();
        }
        public override double CalculateAverageMonthlySalary()
        {
            return 20.8 * 8 * HourlyRate;
        }

        public override string ToString()
        {
            return "Работник  с  почасовой  оплатой " + String.Format("{0," + __Format + "}", HourlyRate)
                    + " среднемесячная заработная плата - " + String.Format("{0," + __Format + "}", AverageMonthlySalary);
        }
        public override string ToString(string format)
        {
            return "Работник  с  почасовой  оплатой " + String.Format("{0," + format + "}", HourlyRate)
                    + " среднемесячная заработная плата - " + String.Format("{0," + format + "}", AverageMonthlySalary);
        }
    }
}
