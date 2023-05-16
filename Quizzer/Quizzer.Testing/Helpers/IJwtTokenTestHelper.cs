using System.Text.RegularExpressions;

namespace Quizzer.Testing.Helpers;

public interface IJwtTokenTestHelper
{
    string Hs256JwtHeader { get; }
    Regex EncodingRegex { get; }
    Regex DotRegex { get; }
    int ExpectedDotCount { get; }
    string XmlSoapClaimPrefix { get; }
    string MicrosoftClaimPrefix { get; }
}