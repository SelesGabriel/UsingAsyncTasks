using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UsingAsyncTasks
{
    class Program
    {
        //static async Task Main(string[] args) //utilizando para chamar métodos assincronos
        static void Main(string[] args) //utilizando para chamar métodos não assincronos
        {
            //Utilizando o WhenAll - O WhenAll serve para executar em segundo plano, todos os métodos em paralelo, e chamar ele quando a execução do anterior estiver ok, 
            //diminuindo o tempo total de carregamento
            //await ExecutaTarefaAsync();
            //await ExecutaTarefasAsyncWhenAll();


            //Cancelando operações assincronas


            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            var semaforo = new Semaforo();
            try
            {
                //Cancelamento de Task
                //Console.WriteLine("Cancelamento automático após 1.5segundos");
                //await ExecutaCancelamento(2000);
                //Console.WriteLine("Cancelamento apos tecla");
                //await ExecutaCancelamentoAposTeclar();

                semaforo.Main();




            }
            catch (Exception)
            {

                //Console.Write($"Tarefa cancelada após {stopwatch.Elapsed}");
            }
            Console.ReadKey();

        }

        

       
    }
}
