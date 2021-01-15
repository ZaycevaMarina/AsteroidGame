using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2
{
    /// <summary>
    /// Класс работников с почасовой и фиксированной оплатой
    /// </summary>
    abstract class Employee : IComparable
    {
        protected double AverageMonthlySalary { get; set; }
        protected static string __Format = "10:0,0.00";
        public Employee()
        {
            //AverageMonthlySalary = CalculateAverageMonthlySalary();
        }

        /// <summary>
        /// Cреднемесячная заработная плата
        /// </summary>
        /// <returns></returns>
        public abstract double CalculateAverageMonthlySalary();
        /// <summary>
        /// Строка значений встроенного формата
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
        /// <summary>
        /// Строка значений указанного формата
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public abstract string ToString(string format);

        /// <summary>
        /// Сравнение двух работников на основании среднемесячной заработной платы
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (AverageMonthlySalary < ((Employee)obj).AverageMonthlySalary) return -1;
            else if (AverageMonthlySalary > ((Employee)obj).AverageMonthlySalary) return 1;
            else
            {
                if (this is EmployeeFixedPayment && obj is EmployeeHourlyPayment)
                    return 1;
                else if (this is EmployeeHourlyPayment && obj is EmployeeFixedPayment)
                    return -1;
                return 0;
            }
        }
    }
}
