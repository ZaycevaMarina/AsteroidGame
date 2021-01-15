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
        public EmployeeHourlyPayment(double hourlyRate):base()
        {
            HourlyRate = hourlyRate;
            AverageMonthlySalary = CalculateAverageMonthlySalary();
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
