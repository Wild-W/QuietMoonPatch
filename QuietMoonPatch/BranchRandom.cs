using System;

public class BranchRandom
{
    public static BranchRandom Instance = new BranchRandom();
    public Random random;

    private const int MAGIC_NUMBER = 7590383;

    BranchRandom()
    {
        random = new Random(MAGIC_NUMBER);
    }
}