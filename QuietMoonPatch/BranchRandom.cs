using System;

public class BranchRandom
{
    public static BranchRandom Instance = new BranchRandom();
    public Random random;

    private const int MAGIC_NUMBER = 117;

    private BranchRandom()
    {
        random = new Random(StartOfRound.Instance.randomMapSeed + MAGIC_NUMBER);
    }
}