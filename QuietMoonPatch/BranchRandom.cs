using System;
using UnityEngine;

public class BranchRandom
{
    public static BranchRandom Instance = new BranchRandom();
    public System.Random random;

    private const int MAGIC_NUMBER = 117;

    private BranchRandom()
    {
        random = new System.Random(StartOfRound.Instance.randomMapSeed + MAGIC_NUMBER);
    }
}