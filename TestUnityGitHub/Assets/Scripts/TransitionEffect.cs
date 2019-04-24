using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEffect : MonoBehaviour
{
    ScreenTransitionImageEffect screenTransitionImageEffect;

    // Start is called before the first frame update
    void Start()
    {
        screenTransitionImageEffect = GetComponent<ScreenTransitionImageEffect>();
        StartCoroutine(transitionTime());
    }

    // Update is called once per frame
    void Update()
    {
        //while(screenTransitionImageEffect.maskValue<=1)
            //screenTransitionImageEffect.maskValue += 0.00001f;
    }

    IEnumerator transitionTime()
    {
        while (screenTransitionImageEffect.maskValue <= 1)
        {
            screenTransitionImageEffect.maskValue += 0.005f;
            yield return new WaitForSeconds(0.000001f);
        }
        yield return new WaitForSeconds(0.1f);
        screenTransitionImageEffect.maskValue = 0;
    }
}
