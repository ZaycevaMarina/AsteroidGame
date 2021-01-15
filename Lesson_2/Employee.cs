using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2
{
    /// <summary>
    /// Класс работников с почасовой и фиксированной оплатой
    /// </summary>
    abstract class Employee
    {
        /// <summary>
        /// Cреднемесячная заработная плата
        /// </summary>
        /// <returns></returns>
        public abstract double CalculateAverageMonthlySalary();
    }
}
