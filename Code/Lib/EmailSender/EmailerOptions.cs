namespace EmailSender;

public class EmailerOptions
{
    public string? ToAddress { get; init; }
    public string? FromAddress { get; init; }
    public string? BodyFilePath { get; init; }
    public string? Subject { get; init; }
    public int? NumIterations { get; init; }
    public int? NumSecondsBetweenIterations { get; init; }
    public string? FromPassword { get; init; }
}
