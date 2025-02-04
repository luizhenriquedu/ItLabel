namespace CSharpGit.Exceptions;

public class NoMessageToCommitException(string message) : Exception(message);