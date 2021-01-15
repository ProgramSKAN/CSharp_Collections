using System;
using System.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Collections._1.Collections_And_Atomic_Operations
{
    //for source code of Enqueue https://source.dot.net/#q=enqueue
    //-------ATOMICITY & ATOMIC OPERATIONS---------------
    //For thread safety following condition need to satisfy
    //1.we need guarantee that when CPU swaps execution to different thread, that new thread won't ever see partially modified data from halfway through a method
    //ie.,for thread-safety, you need an operation to appear to all other threads as if it's instantaneous.
    //Any other thread should always see the operation either not started or as completed.never as half done.
    //2.An Atomic method should guarantee that it will always either complete successfully or if it does fail, it will fail without making any changes to the data its working on.no matter what other threads are doing at the time.

    //An ATOMIC METHOD Stisfies above requirements 1)Graranteed Instantaneous to other Threads. 2)Guaranteed Success or fails cleanly.
    //Queue<T>.Enqueue() ,STANDARD COLLECTIONS              > not Atomic
    //ConcurrantQueue<T>.Enqueue(), CONCURRANT COLLECTIONS  >it is ATOMIC
    public class Coll_CoQueue
    {
        public static void Run()
        {
            //dont use STANDARD COLLECTIONS in asynchronous environment because of "Data Corruption" & "Methods are Not Atomic".

            Queue<string> ordersQueue0 = new Queue<string>();
            //Since Queue<T> is not thread-safe, ordersQueue in multithreading sometimes works fine,sometimes exceptions,sometimes missing data
            //Because of Queue<T>.Enqueue() method
            /*
                Enqueue(){      <---Queue is Ready to Use (consistant state)
                    int newcapacity=...         |}
                    setcapacity()               |}<-----while Enqueue() is being executed you will have a short period of time when some of the fields have been updated and others haven't been.
                    _array[tail]=item           |}      the reason thats the big problem is the queue is been used by morethan one is that the CPU to suspend a thread and giv processing time to different thread at any time including when the thread is halfway through executing a method.so, fields in the methods will be out of sync.causes Queue corruption.
                    _size++;
                }               <---Queue is Ready to Use(consistant state)
             */
            //REMEDY: use ConcurrentQueue<T>


            ConcurrentQueue<string> ordersQueue = new ConcurrentQueue<string>();
            //Just changing Queue<T> to ConcurrentQueue<T> is not enough always
            /*---In Multithreded Apps-------
             * single-threaded login often doesn't work
             * Collections operations don't always work the same way
             * Hence, Concurrent collections expose differenc methods
             * Understanding those differences is key to using Concurrent collections
             */




            Task task1 = Task.Run(() => PlaceOrders(ordersQueue, "Jack", 5));
            Task task2 = Task.Run(() => PlaceOrders(ordersQueue, "Anna", 5));

            Task.WaitAll(task1, task2);

            //order of items in ordersQueue is not same for every execution because 2tasks run parallelly
            //even though sequence within each thread is guaranted, But there might be different order between 2 threads.
            //Thread timing uncertainities caused these different results 
            foreach (string order in ordersQueue)
                Console.WriteLine("ORDER: " + order);

            //Single threaded app >  order of operations is guaranteed
            //Concurrant app > order of operations may not be guaranteed
        }
        static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int noOfOrders)
        {
            for (int i = 1; i <= noOfOrders; i++)
            {
                //Thread.Sleep(1);//1ms
                Thread.Sleep(new TimeSpan(1));//1Tick or 100ns
                string orderName = $"{customerName} wants T-Shirts {i}";
                orders.Enqueue(orderName);
            }
        }
    }
}
