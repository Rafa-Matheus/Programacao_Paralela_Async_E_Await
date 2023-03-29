using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async_E_Await
{
    class Program
    {
        static async Task<string> CompraBoloAsync(string nomeDoResponsavel)
        {
            Console.WriteLine($"{nomeDoResponsavel} foi comprar um bolo. {DateTime.Now.ToString("HH:mm:ss tt")}");
            await Task.Delay(2000); // Dentro de método async, não se pode usar Thread.Sleep() para provocar delays
            Console.WriteLine($"{nomeDoResponsavel} já comprou o bolo. {DateTime.Now.ToString("HH:mm:ss tt")}");
            await Task.Delay(2000);
            Console.WriteLine($"{nomeDoResponsavel} voltou para casa com o bolo. {DateTime.Now.ToString("HH:mm:ss tt")}");
            return "Chocolate";
        }

        static async Task<string> CompraBexigaAsync(string nomeDoResponsavel)
        {
            Console.WriteLine($"{nomeDoResponsavel} foi comprar bexigas. {DateTime.Now.ToString("HH:mm:ss tt")}");
            await Task.Delay(2000); // Dentro de método async, não se pode usar Thread.Sleep() para provocar delays
            Console.WriteLine($"{nomeDoResponsavel} já comprou as bexigas. {DateTime.Now.ToString("HH:mm:ss tt")}");
            await Task.Delay(2000);
            Console.WriteLine($"{nomeDoResponsavel} voltou para casa com as bexigas. {DateTime.Now.ToString("HH:mm:ss tt")}");
            return "Vermelha";
        }

        static async void OrganizaFesta()
        {
            /*
             * Abaixo, duas tasks são criadas para as chamadas assíncronas dos métodos
            "CompraBoloAsync" e "CompraBexigaAsync", e mais abaixo, a palavra-chave
            "await" é usada para esperar que essas tasks sejam concluídas antes de
            imprimir o resultado.
             * Eu estou atribuindo às Task<string> as chamadas dos métodos, e ao mesmo
            tempo disparando a execução deles. Eu não estou apenas declarando uma variável
            que possui o valor de uma chamada de método.
            */
            Task<string> compraBoloTask = CompraBoloAsync("Rafael");
            Task<string> compraBexigaTask = CompraBexigaAsync("Carla");


            /*
             * Obs.: Await só pode estar contido dentro de um método assíncrono.
             * Abaixo, o "await" faz aguardar o retorno da operação assíncrona,
            sem travar a execução do programa. Eu poderia atribuir à variável
            a chamada do método diretamente, como é sugerido nos comentários das
            respectivas linhas, mas isso perderia a vantagem que permite que
            as chamadas assíncronas sejam iniciadas antes do "await" ser usado,
            vantagem esta que está no uso das Tasks.
             * Se eu atribuir à variável string a chamada do método diretamente,
            como sugerido nos comentários, seriam executados paralelamente a Thread
            Main e o método assíncrono CompraBoloAsync até o mesmo oferecer retorno,
            isso por sua vez, faria com que o outro método assíncrono "CompraBexigaAsync",
            fique aguardando a sua chamada ao invés de correr em paralelo junto com
            a Main e o CompraBoloAsync.
            */
            string retornoCompraBoloAsync = await compraBoloTask; //CompraBoloAsync("Rafael");
            string retornoCompraBexigaAsync = await compraBexigaTask; // CompraBexigaAsync("Carla");

            Console.WriteLine($"O sabor do bolo é: {retornoCompraBoloAsync}");
            Console.WriteLine($"A cor da bexiga é: {retornoCompraBexigaAsync}");
        }

        static void Main(string[] args)
        {
            /*
             * Se os métodos assíncronos abaixo forem chamados diretamente, o que é
            possível fazer conforme se vê neste comentário, perde-se/desperdiça-se
            o retorno dos mesmos.

            CompraBoloAsync("Rafael");
            CompraBexigaAsync("Carla");
            */

            // Método async que permitirá obter o retorno dos outros métodos assíncronos:
            OrganizaFesta();

            // Laço apenas para confirmar/verificar o paralelismo:
            for(int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"Timer: {i}s");
                Thread.Sleep(1000);
            }

            Console.ReadKey();
        }
    }
}
