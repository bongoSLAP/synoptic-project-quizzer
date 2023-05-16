namespace Quizzer.Interfaces;

public interface IScryptEncoder
{
    bool Compare(string first, string second);
    string Encode(string toEncode);
}