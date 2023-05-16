using System.Text.RegularExpressions;

namespace Quizzer.Testing.Helpers;

public class JwtTokenTestHelper: IJwtTokenTestHelper
{
    public Regex EncodingRegex => new Regex(@"^[a-zA-Z0-9\.\-_]+$");
    public Regex DotRegex => new Regex(@"\.");
    public int ExpectedDotCount => 2;
    public string XmlSoapClaimPrefix => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
    public string MicrosoftClaimPrefix => "http://schemas.microsoft.com/ws/2008/06/identity/claims/";
    public string Hs256JwtHeader => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
}