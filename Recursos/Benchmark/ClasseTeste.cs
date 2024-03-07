using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;

namespace Benchmark;

/*
 * também é possível rodar o mesmo benchmark para diferentes versões do dotnet
 * você deve possuir os dotnet especificados em seu ambiente
 * [SimpleJob(RuntimeMoniker.Net70)]
 * [SimpleJob(RuntimeMoniker.Net80)]
 */
[RankColumn(NumeralSystem.Roman)] //? adiciona uma coluna rankeando o melhor método
public class ClasseTeste
{
    [Benchmark] //? atributo para medição
    public int LoopCem() //? a biblioteca não aceita funções estáticas
    {
        var valor = 0;

        for(int i = 0; i < 100; i++)
        {
            valor++;
        }

        return valor;
    }

    [Benchmark]
    public int LoopMil()
    {
        var valor = 0;

        for (int i = 0; i < 1000; i++)
        {
            valor++;
        }

        return valor;
    }
}
