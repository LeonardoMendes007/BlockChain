using BlockChain.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BlockChain.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlockChainController : ControllerBase
{
    public readonly BlockChain.Core.BlockChain _blockChain;
    public BlockChainController(Core.BlockChain blockChain)
    {
        _blockChain = blockChain;
    }

    [HttpGet("isValid")]
    public IActionResult IsValid()
    {
        var isValid = _blockChain.IsChainValid();

        var response = new BlockChainIsValidResponse(isValid);

        return Ok(response);
    }

    [HttpGet("chain")]
    public IActionResult GetChain()
    {
        var response = new BlockChainResponse()
        {
            Chain = _blockChain.Chain,
            Length = _blockChain.Chain.Count
        };

        return Ok(response);
    }

    [HttpPost("mine")]
    public IActionResult MineBlock([FromBody] string data)
    {
        var previousBlock = _blockChain.GetPreviusBlock();
        var previousProof = previousBlock.Proof;
        var proof = _blockChain.ProofOfWork(previousProof);
        var previousHash = _blockChain.Hash(previousBlock);
        var block = _blockChain.CreateBlock(proof, previousHash, data);

        return Ok(
            new 
            {
                Block = block,
                Message = "Block minerado com sucesso!",
                
            }
        );
    }

}
