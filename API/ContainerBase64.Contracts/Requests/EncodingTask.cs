namespace ContainerBase64.Contracts.Requests;

public class EncodingTask
{
    public required string Key { get; set; }
    public required string Input { get; set; }
    public required CancellationTokenSource CancellationTokenSource { get; set; }
}
