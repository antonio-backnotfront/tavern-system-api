namespace TavernSystem_Training_1.Application.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string msg) : base(msg)
    {
    }
}