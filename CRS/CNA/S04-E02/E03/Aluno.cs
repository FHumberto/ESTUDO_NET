namespace S04_E02.E03
{
    internal class Aluno
    {
        public string? Nome;
        public float[] Notas = new float[3];

        public double NotaFinal()
        {
            return Notas[0] + Notas[1] + Notas[2];
        }
    }
}