using DunGen;
using DunGen.Graph;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixDungeonGeneration : MonoBehaviour
{
    public DungeonFlow flow;
    // An eldritch deity bestowed their curse upon my mod and I must pay for sins unknown by writing this god awful code.
    private void Awake()
    {
        try
        {
            Debug.Log("Fixing dungeon generator");
            RuntimeDungeon runtimeDungeon = FindObjectOfType<RuntimeDungeon>();
            runtimeDungeon.Generator.DungeonFlow = flow;
            FindComponentInScene<RoundManager>(SceneManager.GetSceneByName("SampleSceneRelay")).dungeonGenerator = runtimeDungeon;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    T FindComponentInScene<T>(Scene scene) where T : MonoBehaviour
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            T component = go.GetComponentInChildren<T>(true);
            if (component != null)
            {
                return component;
            }
        }
        return null;
    }
}
