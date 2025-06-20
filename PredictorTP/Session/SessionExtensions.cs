namespace PredictorTP.Session;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string clave, T valor)
    {
        session.SetString(clave, JsonSerializer.Serialize(valor));
    }
    public static T Get<T>(this ISession session, string clave)
    {
        var valor = session.GetString(clave);
        if (valor == null)
            return default(T);
        else
            return JsonSerializer.Deserialize<T>(valor);
    }
}
