namespace ItLabel.Exceptions;

public class NoMessageToCommitException(string message) : Exception(message);