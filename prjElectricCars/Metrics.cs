using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjElectricCars
{
    internal class Metrics
    {
        public Stopwatch Sw { get; set; }
        public int Method { get; set; }

        public Metrics(int method)
        {
            Sw = new Stopwatch();
            Method = method;
        }

        public void StartClock()
        {
            Console.WriteLine($"Execução do método [{Method}] na Thread [{Thread.CurrentThread.ManagedThreadId}] iniciada!");
            Sw.Start();
            Console.WriteLine($"Cronometro iniciado no método [{Method}]");
        }

        public void EndClock()
        {
            Sw.Stop();
            Console.WriteLine($"Tempo gasto na execução do método [{Method}]: [{Sw.ElapsedMilliseconds}ms]");
            Sw.Reset();
            Console.WriteLine($"Execução do método [{Method}] na Thread [{Thread.CurrentThread.ManagedThreadId}] finalizada!\n\n");
        }
    }
}
