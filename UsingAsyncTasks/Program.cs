using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UsingAsyncTasks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Utilizando o WhenAll - O WhenAll serve para executar em segundo plano, todos os métodos em paralelo, e chamar ele quando a execução do anterior estiver ok, 
            //diminuindo o tempo total de carregamento
            //await ExecutaTarefaAsync();
            //await ExecutaTarefasAsyncWhenAll();


            //Cancelando operações assincronas


            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            try
            {
                //Cancelamento de Task
                //Console.WriteLine("Cancelamento automático após 1.5segundos");
                //await ExecutaCancelamento(2000);
                //Console.WriteLine("Cancelamento apos tecla");
                //await ExecutaCancelamentoAposTeclar();






            }
            catch (Exception)
            {

                //Console.Write($"Tarefa cancelada após {stopwatch.Elapsed}");
            }
            Console.ReadKey();

        }

        

       
    }
}
