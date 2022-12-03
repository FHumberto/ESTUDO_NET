namespace S15_E02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\File.csv";

            Console.WriteLine("Chose an option:");
            Console.WriteLine("0: To write custom path where TXT file is.");
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
                using (StreamReader sr = File.OpenText(sourceFilePath))
                {
                    Dictionary<string, int> dictionary = new Dictionary<string, int>();

                    while (!sr.EndOfStream)
                    {
                        string[] votingRecord = sr.ReadLine().Split(',');
                        string candidate = votingRecord[0];
                        int votes = int.Parse(votingRecord[1]);

                        if (dictionary.ContainsKey(candidate))
                        {
                            dictionary[candidate] += votes;
                        }
                        else
                        {
                            dictionary[candidate] = votes;
                        }
                    }
                    foreach (KeyValuePair<string, int> item in dictionary)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}