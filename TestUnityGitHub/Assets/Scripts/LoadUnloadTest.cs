using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnloadTest : MonoBehaviour
{
    public GameObject unloadGameObject;

    // Start is called before the first frame update
    void Start()
    {
        delete();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void delete()
    {
        Resources.UnloadAsset(unloadGameObject);
    }
}
