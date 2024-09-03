namespace BlockChain.API.Responses;

public class BlockChainIsValidResponse
{
    public string Message { get => IsValid ? "BlockChain is valid!" : "BlockChain is invalid!"; }
    public bool IsValid { get; set; }

    public BlockChainIsValidResponse(bool isValid)
    {
        IsValid = isValid;
    }
}
