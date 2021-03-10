using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingAsyncTasks
{
    public class CancelaTask
    {
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
                var tecladoTask = Task.Run(() =>
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
