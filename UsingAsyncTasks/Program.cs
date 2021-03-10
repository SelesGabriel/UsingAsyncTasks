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


            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                //Console.WriteLine("Cancelamento automático após 1.5segundos");
                //await ExecutaCancelamento(2000);
                Console.WriteLine("Cancelamento apos tecla");
                await ExecutaCancelamentoAposTeclar();
            }
            catch (Exception)
            {

                Console.Write($"Tarefa cancelada após {stopwatch.Elapsed}");
            }
            Console.ReadKey();

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


        //Cancelamento de operação
        public static async Task ExecutaCancelamento(int tempo)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(tempo);
                try
                {
                    var resultado = await OperacaoLongauracaoCancelavel(100, cancellationTokenSource.Token);
                    Console.WriteLine("Resultado: " + resultado);
                }
                catch (TaskCanceledException)
                {

                    throw;
                }
            }
        }


        public static async Task ExecutaCancelamentoAposTeclar()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var tecladoTask = Task.Run(()=>
                {
                    Console.WriteLine("Pressione uma tecla para cancelar");
                    Console.ReadKey();
                    cancellationTokenSource.Cancel();
                });

                try
                {
                    var tarefa = OperacaoLongauracaoCancelavel(500, cancellationTokenSource.Token);
                    var resultado = await tarefa;
                    Console.WriteLine($"Resultado {resultado}");
                }
                catch (TaskCanceledException)
                {

                    throw;
                }
                await tecladoTask;
            }
        }

        private static Task<int> OperacaoLongauracaoCancelavel(int valor, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Executou Operacao de Longa duração");

            Task<int> task = null;

            task = Task.Run(() =>
            {
                int resultado = 0;
                for (int i = 0; i < valor; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);
                    

                    Thread.Sleep(10);
                    resultado += i;
                }
                return resultado;
            });
            return task;
        }
    }
}
