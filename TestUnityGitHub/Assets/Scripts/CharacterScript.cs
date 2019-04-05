using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public TextMesh statusText;

    private bool isTiming = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTiming)
        {
            if(timer >= 2)
            {
                statusText.text = "Character";
            }else
            {
                timer += Time.deltaTime;
            }
        } else
        {
            timer = 0;
            statusText.text = "";
        }
    }

    private void OnBecameVisible()
    {
        isTiming = true;
    }

    private void OnBecameInvisible()
    {
        isTiming = false;
    }
}
