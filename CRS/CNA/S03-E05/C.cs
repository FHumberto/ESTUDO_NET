namespace S03_E05
{
    internal static class C
    {
        public static void CMain()
        {
            Console.WriteLine("Ler o tipo de combustível abastecido (codificado da seguinte forma:");
            Console.WriteLine("1.Álcool 2.Gasolina 3.Diesel 4.Fim");
            Console.WriteLine("Caso o usuário informe um código inválido (fora da faixa de 1 a 4) deve ser solicitado um novo código (até que seja válido)");
            Console.WriteLine("O programa será encerrado quando o código informado for o número 4.");
            Console.WriteLine("Deve ser escrito a mensagem: MUITO OBRIGADO e a quantidade de clientes que abasteceram cada tipo de combustível, conforme exemplo");

            int aux = 0;
            int[] x = new int[3];

            while (aux != 4)
            {
                aux = int.Parse(Console.ReadLine());
                switch (aux)
                {
                    case 1:
                        x[0]++;
                        break;

                    case 2:
                        x[1]++;
                        break;

                    case 3:
                        x[2]++;
                        break;

                    case 4:
                        Console.WriteLine("MUITO OBRIGADO");
                        Console.WriteLine($"Alcool: {x[0]}");
                        Console.WriteLine($"Gasolina: {x[1]}");
                        Console.WriteLine($"Diesel: {x[2]}");
                        break;
                }
            }
        }
    }
}