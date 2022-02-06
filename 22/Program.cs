using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        

        static void Main()
        
        {
            object n = Convert.ToInt32(Console.ReadLine());
            Func<object, int[]> func1 = new Func<object,int[]>(MyArray);
            Task<int[]> task1 = new Task<int[]>(func1,n);
            task1.Start();

            Func< Task<int[]>,int> func2 = new Func<Task<int[]>,int>(Summa);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func< Task<int>, object, double> func3 = new Func<Task<int>, object, double>(Temp);
            Task<double> task3 = task2.ContinueWith<double>(func3,n);

            Console.ReadKey();
        }
        static int[] MyArray (object n)
        {
            int a = (int)n;
            int[] array = new int[a];
            Random random = new Random();
            for (int i = 0; i < a; i++)
            {
                array[i] = random.Next(0, 10);
                Console.Write("{0} ", array[i]);
            }
            return array;
        }
        static int Summa (Task<int[]> task)
        {
            int[] arr = task.Result;
            int S = 0;
            for (int i = 0;i<arr.Count()-1;i++)
                S += arr[i];
            Console.WriteLine("\n");
            Console.WriteLine(S);
            return S;
        }
        static double Temp (Task<int> task2, object n)
        {
            double S1 = (int)task2.Result;
            double a = (int)n;
            double T = S1 / a;
            Console.WriteLine(T);
            return T;
        }
    }
}
