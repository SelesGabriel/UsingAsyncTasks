using System;
using System.Threading;



namespace UsingAsyncTasks
{
    public class Semaforo
    {
        public Semaphore semaphore { get; set; } = new Semaphore(300, 500);
        public SemaphoreSlim semaphoreSlim { get; set; } = new SemaphoreSlim(40);
        
        public void Main()
        {
            //semaphoreClass();
            semaphoreSlimClass();
        }

        public void ProcessarOperacao()
        {
            semaphore.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} entrou na sessão crítica");
            Thread.Sleep(6000);
            semaphore.Release();
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} foi liberada");
        }


        public void semaphoreClass()
        {

            for (int i = 0; i < 10000; i++)
            {
                Thread thread = new Thread(new ThreadStart(ProcessarOperacao));
                thread.Name = "Thread: " + i;
                thread.Start();
            }
            Console.ReadLine();
        }

        public void semaphoreSlimClass()
        {
            for(int i = 1; i < 700; i++)
            {
                string threadName = "Thread " + i;
                int espera = 2 + 2 * i;

                var t = new Thread(() => AcessarBancoDados(threadName, espera));

                t.Start();
            }
        }

        void AcessarBancoDados(string nome, int seconds)
        {
            Console.WriteLine($"{nome} aguardando acesso ao banco de dados");
            semaphoreSlim.Wait();

            Console.WriteLine($"{nome} autorizada a acessar o banco de dados");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            Console.WriteLine($"{nome} concluída");
            semaphoreSlim.Release();
        }


    }
}
