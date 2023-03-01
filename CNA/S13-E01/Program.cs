using S13_E01.Entities;

using System.Globalization;

namespace S13_E01
{
    internal static class Program
    {
        public static void Main()
        {
            //diretório padrão CNA\S13-E01\File.csv
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\File.csv";

            Console.WriteLine("Chose an option:");
            Console.WriteLine("0: To write custom path where CSV file is.");
            Console.WriteLine("1: To use default location");

            int opt = int.Parse(Console.ReadLine());
            string? sourceFilePath = null;

            switch (opt)
            {
                case 0:
                    Console.Write("Enter file full path: ");
                    sourceFilePath = Console.ReadLine();
                    break;

                case 1:
                    sourceFilePath = defaultPath;
                    break;

                default:
                    Console.WriteLine("This option does not exist. Please try again.");
                    break;
            }

            try
            {
                //Ler todas as linhas do arquivo
                string[] lines = File.ReadAllLines(sourceFilePath);

                string? sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                string? targetFolderPath = sourceFolderPath + @"\out";
                string? targetFilePath = targetFolderPath + @"\summary.csv";

                //cria uma pasta usando o diretório informado + diretorio adicional
                Directory.CreateDirectory(targetFolderPath);

                //ler os dados do arquivo
                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string line in lines)
                    {
                        string[] fields = line.Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product prod = new(name, price, quantity);

                        sw.WriteLine(prod); //Imprime na tela os dados do arquivo
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}