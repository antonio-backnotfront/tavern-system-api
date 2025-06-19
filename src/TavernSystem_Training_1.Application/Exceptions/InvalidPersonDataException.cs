namespace TavernSystem_Training_1.Application.Exceptions;

public class InvalidPersonDataException : Exception
{
    public InvalidPersonDataException(string msg) : base(msg)
    {
    }
}