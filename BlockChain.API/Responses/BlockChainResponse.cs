using BlockChain.Core.Models;

namespace BlockChain.API.Responses;

public class BlockChainResponse
{
    public IList<Block> Chain { get; set; }
    public int Length { get; set; }
}
