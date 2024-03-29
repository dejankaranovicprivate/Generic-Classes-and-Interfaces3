﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectIt
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeList = CreateCpllection(typeof(List<>), typeof(Employee));
            Console.Write(employeeList.GetType().Name);
            var genericArguments = employeeList.GetType().GenericTypeArguments;
            foreach(var arg in genericArguments)
            {
                Console.Write("[{0}]", arg.Name);
            }
            Console.WriteLine();

            var employee = new Employee();
            var employeeType = typeof(Employee);
            var methodInfo = employeeType.GetMethod("Speak");
            methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));
            methodInfo.Invoke(employee, null);

            Console.ReadLine();
        }

        private static object CreateCpllection(Type collectionType, Type itemType)
        {
            var closedType = collectionType.MakeGenericType(itemType);
            return Activator.CreateInstance(closedType);
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public void Speak<T>()
        {
            Console.WriteLine(typeof(T).Name);
        }
    }
}
