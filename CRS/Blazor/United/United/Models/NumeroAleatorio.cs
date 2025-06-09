namespace United.Models
{
    public class NumeroAleatorio
    {
        public int NumeroIdentificador { get; set; }

        public NumeroAleatorio()
        {
            Random rmd = new Random();
            NumeroIdentificador = rmd.Next(0, 10000);
        }
    }
}
