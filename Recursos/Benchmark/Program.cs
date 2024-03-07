using BenchmarkDotNet.Running;

namespace Benchmark;

//? lembrar de rodar a aplicação em modo release
public class Program
{
    static void Main(string[] args)
    {
        //? o método abaixo chama a classe de teste e os anotations mapeados
        BenchmarkRunner.Run<ClasseTeste>();
    }
}
