using System.Runtime.CompilerServices;

namespace System;

public class SecurityInvalidKeyException : ArgumentException
{
    public SecurityInvalidKeyException()
        : base()
    {
    }
    
    public SecurityInvalidKeyException(string? paramName)
        : base(paramName)
    {
    }
    
    public SecurityInvalidKeyException(string? message, Exception? innerException)
        : base(message, innerException)
    {
        
    }
    
    public SecurityInvalidKeyException(string? paramName, string? message)
        : base(message, paramName)
    {
        
    }
    
    public static void ThrowIfInsufficient([NotNull]byte[] key, int length, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (key != null && key.Length == length)
            return;
        SecurityInvalidKeyException.Throw(paramName);
    }
    
    [DoesNotReturn]
    internal static void Throw(string? paramName) => throw new SecurityInvalidKeyException(paramName);
}