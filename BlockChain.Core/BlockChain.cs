using BlockChain.Core.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Core;
public class BlockChain 
{
    public IList<Block> Chain { get; set; }

    public BlockChain()
    {
        Chain = new List<Block>();
        CreateBlock(proof: 1, previusHash: "0");
    }

    public Block CreateBlock(int proof, string previusHash, string Data = "")
    {
        var block = new Block
        {
            Index = Chain.Count + 1,
            TimeStamp = DateTime.UtcNow.ToString(),
            Proof = proof,
            PreviousHash = previusHash,
            Data = Data
        };
        Chain.Add(block);
        return block;
    }

    public Block GetPreviusBlock()
    {
        return Chain[Chain.Count - 1];
    }

    public int ProofOfWork(int previusProof)
    {
        var newProof = 1;

        while (true)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes((Math.Pow(newProof, 2) - Math.Pow(previusProof, 2)).ToString()));
            var hashOperation = Convert.ToHexString(bytes);

            if (hashOperation.StartsWith("0000"))
                break;

            newProof++;
        }

        return newProof;
    }

    public string Hash(Block block)
    {
        var encodedBlock = JsonConvert.SerializeObject(block);
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(encodedBlock)));
    }

    public bool IsChainValid()
    {
        var previousBlock = Chain[0];
        var blockIndex = 1;

        while (blockIndex < Chain.Count)
        {
            var block = Chain[blockIndex];
            if (block.PreviousHash != Hash(previousBlock))
            {
                return false;
            }

            var previousProof = previousBlock.Proof;
            var proof = block.Proof;
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes((Math.Pow(proof, 2) - Math.Pow(previousProof, 2)).ToString()));
            var hashOperation = Convert.ToHexString(bytes);

            if (!hashOperation.StartsWith("0000"))
            {
                return false;
            }

            previousBlock = block;
            blockIndex++;
        }

        return true;
    }
}
