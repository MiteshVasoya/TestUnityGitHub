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
    }
    
    private void OnApplicationFocus(bool focus)
    {
        count++;
        Debug.Log("OnApplicationFocus: " + focus);
        if (!focus && count == 2)
        {
            Application.Quit();
        }
    }
}
