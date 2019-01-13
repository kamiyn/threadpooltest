using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace threadpooltest
{
    class Program
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<int, List<DateTimeOffset>> dict = new ConcurrentDictionary<int, List<DateTimeOffset>>();

        static void Main(string[] args)
        {
            Doit().Wait();
            Console.WriteLine("thread count: " + dict.Count);
            return;
        }

        static async Task Doit()
        {
            for (var idx = 0; idx < 100; idx++)
            {
                await waitRandom();
            }
        }

        static async Task waitRandom()
        {
            var now = DateTimeOffset.Now;
            var randMillisec = (int)(new Random().NextDouble() * 100);
            await Task.Delay(randMillisec);

            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            var datetimeList = dict.GetOrAdd(threadId, new List<DateTimeOffset>());
            datetimeList.Add(now);
        }
    }
}
