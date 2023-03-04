namespace S03_E05
{
    internal static class A
    {
        public static void AMain()
        {
            Console.WriteLine("Repita a leitura de uma senha até que ela seja válida.");
            Console.WriteLine("Para cada leitura de senha incorreta informada, escrever a mensagem \"Senha Invalida\".");
            Console.WriteLine("Quando a senha for informada corretamente deve ser impressa a mensagem \"Acesso Permitido\"");
            Console.WriteLine("e o algoritmo encerrado. Considere que a senha correta é o valor 2002");

            int senha = 0;
            while (senha != 2002)
            {
                senha = int.Parse(Console.ReadLine());
                if (senha != 2002)
                {
                    Console.WriteLine("Senha Inválida");
                }
                else
                {
                    Console.WriteLine("Acesso Permitido");
                }
            }
        }
    }
}