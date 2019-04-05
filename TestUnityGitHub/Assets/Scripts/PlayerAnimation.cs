using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("-----PlayerAnimation Start-----");
        animator = GetComponent<Animator>();
    }

    public void walk()
    {
        animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
