﻿namespace Paraglider.Infrastructure.Common.Exceptions;

public class DatabaseInitializerException : Exception 
{
    public DatabaseInitializerException(string message = "Error during database initialization execution") : base(message) { }
}
