using UnityEngine;

public class Level : MonoBehaviour
{
    //Parameters
    [SerializeField] int breakableBlocks; // Serialized for Debugging pusposes

    //Chached Ref
    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public  void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}
