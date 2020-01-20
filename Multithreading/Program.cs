using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Multithreading
{
    class Program
    {
        static int GLOBAL_VALUE = 0;
        static readonly object locker = new object();
    static void Main(string[] args)
        {

            Thread t = new Thread(Increment);
            Thread t2 = new Thread(Increment);
            Thread t3 = new Thread(() => Decrement(5));
            Thread t4 = new Thread(delegate ()
            {
                Decrement(10);
            });
            Thread.CurrentThread.Name = "Main";
            t.Name = "Thread1";
            t2.Name = "Thread2";
            t3.Name = "Thread3";
            t4.Name = "Thread4";
            t.Start();
            //t.Abort();
           // t.Join();
            t2.Start();
           // t2.Join();
            t3.Start();
           // t3.Join();
            t4.Start();
            // t4.Abort();
           // t4.Join();
            Increment();
            Console.WriteLine("Main thread at the end");







            Console.WriteLine("GLOBAL VALUE: " + GLOBAL_VALUE);
            Console.ReadKey();
        }
        static void Increment()
        {
            try
            {
                Console.WriteLine("Name of the Thread: " + Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    lock (locker)
                    {

                            int tmp = GLOBAL_VALUE;
                            tmp++;
                            Console.WriteLine(i);
                            Thread.Sleep(10);
                            GLOBAL_VALUE = tmp;
                            Console.WriteLine("Thread {0}, Global_Value {1}", Thread.CurrentThread.Name, GLOBAL_VALUE);

                    }

                }




            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine("Thread {0} Aborted!", Thread.CurrentThread.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Name of Exception: " + ex);
            }

        }
        static void Decrement(int max)
        {
            
            try
            {

                Console.WriteLine("Name of the Thread: " + Thread.CurrentThread.Name);
                for (int i = max; i >= 0; i--)
                {
                    lock (locker)
                    {

                            int tmp = GLOBAL_VALUE;
                            tmp--;
                            Console.WriteLine(i);
                            GLOBAL_VALUE = tmp;
                            Console.WriteLine("Thread {0}, Global_Value {1}" , Thread.CurrentThread.Name, GLOBAL_VALUE);

                    }

                }



            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine("Thread {0} Aborted!", Thread.CurrentThread.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Name of Exception: " + ex);
            }

        }
    }

}
