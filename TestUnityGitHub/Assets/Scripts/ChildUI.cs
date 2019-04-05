using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildUI : ParentUI
{
    // Start is called before the first frame update
    void Start()
    {
        disappearCanvas(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disappearCanvas(float waitTime)
    {
        StartCoroutine(toggleCanvasVisibilityCoroutine(waitTime));
    }

    private IEnumerator toggleCanvasVisibilityCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hideButton();
    }
}
