using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //parameters
    [SerializeField] int breakableBlocks; //for debugging process only

    //cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader == null) throw new InitializationComponentException(nameof(SceneLoader));

    }
    public void CountBlocks()
    {
        breakableBlocks++;
    }
    public void RemoveBreakableBlocks()
    {
        breakableBlocks--;

        if (breakableBlocks == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
