using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimizedScript : MonoBehaviour
{
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Application.OpenURL("https://www.google.com");
    }
    
    private void OnApplicationFocus(bool focus)
    {
        count++;
        Debug.Log("OnApplicationFocus: " + focus);
        Debug.Log("count: " + count);
        if (!focus)
        {
            Application.Quit();
        }
    }
}
