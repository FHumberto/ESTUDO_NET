namespace S03_E03
{
    static class Index
    {
        static void Main()
        {
            char? e = char.Parse(Console.ReadLine());

            switch (e)
            {
                case 'a':
                    Console.WriteLine("ler dois valores inteiros, e depois mostrar na tela a soma desses números com uma mensagem explicativa:");
                    A.AMain();
                    break;
                case 'b':
                    Console.WriteLine("ler o valor do raio de um círculo, e depois mostrar o valor da área deste círculo com quatro casas decimais:");
                    B.BMain();
                    break;
                case 'c':
                    Console.WriteLine("ler quatro valores inteiros A, B, C e D. A seguir, calcule e mostre a diferença do produto de A e B pelo " +
                        "produto de C e D segundo a fórmula: DIFERENCA = (A * B - C * D)");
                    C.CMain();
                    break;
                case 'd':
                    Console.WriteLine("leia o número de um funcionário, seu número de horas trabalhadas, o valor que recebe por hora e calcula o " +
                        "salário desse funcionário. A seguir, mostre o número e o salário do funcionário, com duas casas decimais");
                    D.DMain();
                    break;
                case 'e':
                    Console.WriteLine("ler o código de uma peça 1, o número de peças 1, o valor unitário de cada peça 1, o  código de uma peça 2, " +
                        "o número de peças 2 e o valor unitário de cada peça 2. Calcule e mostre o valor a ser pago:");
                    E.EMain();
                    break;
                case 'f':
                    Console.WriteLine("Fazer um programa que leia três valores com ponto flutuante de dupla precisão: A, B e C. Em seguida, calcule e mostre:");
                    Console.WriteLine("a) a área do triângulo retângulo que tem A por base e C por altura.");
                    Console.WriteLine("b) a área do círculo de raio C. (pi = 3.14159)");
                    Console.WriteLine("a área do trapézio que tem A e B por bases e C por altura");
                    Console.WriteLine("a área do quadrado que tem lado B.");
                    Console.WriteLine("a área do retângulo que tem lados A e B.");
                    F.FMain();
                    break;
            }
        }
    }
}