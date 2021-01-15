﻿/* Урок 2. Задание 1
 * Построить  три  класса  (базовый  и  2  потомка),  описывающих  работников  с  почасовой  оплатой  (один  из  потомков)
и  фиксированной оплатой (второй потомок):
Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы. 
Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»; 
для  работников  с  фиксированной  оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
 * Создать на базе абстрактного класса массив сотрудников и заполнить его;
 * Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
 * Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
*/
using System;

namespace Lesson_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----Класс работников  с  почасовой  оплатой  (один  из  потомков) и  фиксированной оплатой-----");
            int count_employees = 30;
            Employee[] employees = new Employee[count_employees];
            Random rnd = new Random(100);
            double payment;
            for(int i = 0; i < employees.Length; i++)
            {
                payment = rnd.NextDouble();
                if (i % 2 == 0)
                    employees[i] = new EmployeeHourlyPayment(payment * 1_000);
                else
                    employees[i] = new EmployeeFixedPayment(payment * 200_000);
            }
            Console.WriteLine("Массив из {count_employees} сотрудников:");
            foreach(Employee emp in employees)
            {
                Console.WriteLine(emp.ToString());
            }


                Console.WriteLine("Для завершния нажмите любую кнопку...");
            Console.ReadKey();
        }
    }

}
