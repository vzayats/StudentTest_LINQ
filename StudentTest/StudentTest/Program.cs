using System;

namespace StudentTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logic = new TestLogic();
            Console.WriteLine("Список студентов, которые прошли тесты:");
            Console.WriteLine();
            logic.Query1();

            Console.WriteLine("Список тех, кто прошли тесты успешно и уложилися во время:");
            Console.WriteLine();
            logic.Query2();

            Console.WriteLine("Список студентов, которые прошли тесты успешно но не уложились во время:");
            Console.WriteLine();
            logic.Query3();

            Console.WriteLine("Список студентов по городам. (Из Львова: 10 студентов, из Киева: 20):");
            Console.WriteLine();
            logic.Query4();

            Console.WriteLine("Список успешных студентов по городам:");
            Console.WriteLine();
            logic.Query5();

            Console.WriteLine("Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории:");
            Console.WriteLine();
            logic.Query6();

            Console.ReadKey();
        }
    }
}
