using System.Text;
using System.Text.Json;

public class JwtHelper
{
    public static IDictionary<string, object> ParseJwt(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException(nameof(token), "Token cannot be null or empty");
        }

        // Extrahera payload (mittendelen av token)
        var parts = token.Split('.');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Token must have exactly three parts separated by dots.");
        }

        var base64Url = parts[1];
        var base64 = base64Url.Replace('-', '+').Replace('_', '/');

        var bytes = Convert.FromBase64String(PadBase64String(base64));
        var jsonPayload = Encoding.UTF8.GetString(bytes);

        return JsonSerializer.Deserialize<IDictionary<string, object>>(jsonPayload)!;
    }

    private static string PadBase64String(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: return base64 + "==";
            case 3: return base64 + "=";
            default: return base64;
        }
    }
}
