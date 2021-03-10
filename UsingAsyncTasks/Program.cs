using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UsingAsyncTasks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await ExecutaTarefaAsync();
            await ExecutaTarefasAsyncWhenAll();
        }

        private static async Task ExecutaTarefasAsyncWhenAll()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var dadosTask = ExtrairDadosAsync();
            var resultadoTask = InserirDadosAsync();

            await Task.WhenAll(dadosTask, resultadoTask);

            Console.WriteLine("Tempo gasto para extrair e inserir dados.. " + stopwatch.Elapsed);
            Console.WriteLine("Resultado de extrair dados = " + dadosTask.Result);
            Console.WriteLine("Resultado de inserir dados = " + resultadoTask.Result);
        }


        private static async Task ExecutaTarefaAsync()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var dados = await ExtrairDadosAsync();
            Console.WriteLine("Tempo para extrair dados... " + stopWatch.Elapsed);

            var resultado = await InserirDadosAsync();
            Console.WriteLine("Tempo gasto para inserir dados.. " + stopWatch.Elapsed + "\n");
        }
        private static async Task<string> ExtrairDadosAsync()
        {
            string dados = "Dados";
            Console.WriteLine("Extrair dados executou");
            await Task.Delay(TimeSpan.FromSeconds(2));
            return dados;
        }

        private static async Task<bool> InserirDadosAsync()
        {
            Console.WriteLine("\n Inserir dados Executou");
            var resultado = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            return resultado;
        }
    }
}
 