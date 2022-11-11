namespace S03_E04
{
    static class Index
    {
        public static void Main()
        {
            Console.WriteLine("Informe a letra do problema: [a, b, c, d, e, f]");
            char? a = char.Parse(Console.ReadLine().ToLower());

            switch (a)
            {
                case 'a':
                    Console.WriteLine("ler um número inteiro, e depois dizer se este número é negativo ou não");
                    A.AMain();
                    break;
                case 'b':
                    Console.WriteLine("ler um número inteiro e dizer se este número é par ou ímpar");
                    B.BMain();
                    break;
                case 'c':
                    Console.WriteLine("Leia 2 valores inteiros (A e B). Após, o programa deve mostrar uma mensagem Sao Multiplos ou Nao sao Multiplos, indicando se os valores lidos são múltiplos entre si. Atenção: os números devem poder ser digitados em ordem crescente ou decrescente");
                    C.CMain();
                    break;
                case 'd':
                    Console.WriteLine("Leia a hora inicial e a hora final de um jogo. A seguir calcule a duração do jogo, sabendo que o mesmo pode\r\ncomeçar em um dia e terminar em outro, tendo uma duração mínima de 1 hora e máxima de 24 horas");
                    D.DMain();
                    break;
                case 'e':
                    Console.WriteLine("escreva um programa que leia o código de um item e a quantidade deste item. A seguir, calcule e mostre o valor da conta a pagar");
                    E.EMain();
                    break;
                case 'f':
                    Console.WriteLine("ler um valor qualquer e apresente uma mensagem dizendo em qual dos seguintes intervalos ([0,25], (25,50], (50,75], (75,100]) este valor se encontra. Obviamente se o valor não estiver em nenhum destes intervalos, deverá ser impressa a mensagem “Fora de intervalo");
                    F.FMain();
                    break;
                case 'g':
                    Console.WriteLine("Leia 2 valores com uma casa decimal (x e y), que devem representar as coordenadas de um ponto em um plano. A seguir, determine qual o quadrante ao qual pertence o ponto, ou se está sobre um dos eixos cartesianos ou na origem (x = y = 0). Se o ponto estiver na origem, escreva a mensagem “Origem”. Se o ponto estiver sobre um dos eixos escreva “Eixo X” ou “Eixo Y”, conforme for a situação");
                    G.GMain();
                    break;
                case 'H':
                    Console.WriteLine("Em um país imaginário denominado Lisarb, todos os habitantes ficam felizes em pagar seus impostos, pois sabem que nele não existem políticos corruptos e os recursos arrecadados são utilizados em benefício da população, sem qualquer desvio. A moeda deste país é o Rombus, cujo símbolo é o R$.");
                    H.HMain();
                    break;
                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}
