using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLLauncher : MonoBehaviour
{
    private bool isReturnFromBrowser = false;
    private bool isOpen = false;
    public TextMesh text;

    // Start is called before the first frame update
    void Start()
    {
        open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        Debug.Log("Opening URL");
        Application.OpenURL("https://dl-web.wright.edu/lcg?profile=one");
        isOpen = true;
    }

    int i = 0;

    private void OnApplicationFocus(bool focus)
    {
        
        if (focus)
        {
            if (isReturnFromBrowser && i>1)
            {
                isReturnFromBrowser = false;
                text.text = "Return "+ i;
            }
            i++;
        }
        else
        {
            if (isOpen)
                isReturnFromBrowser = true;
        }
        
    }
}
