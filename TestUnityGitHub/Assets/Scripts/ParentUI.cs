using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentUI : MonoBehaviour
{
    private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideButton()
    {
        button = GameObject.Find("/Canvas/Button");
        button.SetActive(false);
    }
}
