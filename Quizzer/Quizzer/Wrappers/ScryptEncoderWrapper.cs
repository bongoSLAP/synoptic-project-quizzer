using Quizzer.Interfaces;
using Scrypt;

namespace Quizzer.Wrappers;

public class ScryptEncoderWrapper : IScryptEncoder
{
    private readonly ScryptEncoder _encoder;

    public ScryptEncoderWrapper(ScryptEncoder encoder)
    {
        _encoder = encoder;
    }

    public bool Compare(string first, string second)
    {
        return _encoder.Compare(first, second);
    }
    
    public string Encode(string toEncode)
    {
        return _encoder.Encode(toEncode);
    }
}