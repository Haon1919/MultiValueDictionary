using System;

public class DuplicateMemberException : Exception
{
    public DuplicateMemberException()
    {
    }

    public DuplicateMemberException(string message)
        : base(message)
    {
    }

    public DuplicateMemberException(string message, Exception inner)
        : base(message, inner)
    {
    }
}