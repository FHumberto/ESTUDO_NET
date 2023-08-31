namespace MagicVilla_Utility;

public static class SD
{
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    //! MÉTODO ESTÁTICO INCLUIDO, PARA ADICIONAR A TODAS AS CHAMADAS
    public static string SessionToken = "JWTToken";
}
