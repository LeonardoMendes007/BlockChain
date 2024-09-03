namespace BlockChain.Core.Models;
public class Block
{
    public int Index { get; set; }
    public string TimeStamp { get; set; }
    public int Proof { get; set; }
    public string PreviousHash { get; set; }
    public string Data { get; set; }

}
