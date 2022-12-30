namespace CNA_SalesWebMvc.Services.Exceptions
{
    public class DBConcurrencyException : ApplicationException
    {
        public DBConcurrencyException(string message) : base(message)
        {
        }
    }
}