namespace CSharpGit.Exceptions;

public class RepositoryNotFoundException(string message) : Exception(message);