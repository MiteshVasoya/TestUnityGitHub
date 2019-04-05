using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private int currentWayPointIndex = 0;
    float radius = 1;
    UnityEngine.AI.NavMeshAgent nav;
    Animator animator;

    private float timer;
    private int waitTime;
    private bool isTimer;
    private int angularSpeed;

    private GameObject[] wayPoints;

    private bool isWalking = false;

    private void Start()
    {
        Debug.Log("-----PlayerAction Start-----");
        isTimer = false;
        angularSpeed = 0;
        timer = 0;
        this.nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.animator = GetComponent<Animator>();
    }

    public void goToDestination(GameObject[] wayPoints)
    {
        startWalkingWithWaiting(wayPoints, 0);
    }

    public void startWalkingWithWaiting(GameObject[] wayPoints, int waitTime)
    {
        if (this.nav != null && this.animator != null)
        {
            this.wayPoints = wayPoints;
            this.waitTime = waitTime;
            this.currentWayPointIndex = 0;
            animator.SetBool("isWalking", false);
            isTimer = true;
        }
    }

    void Update()
    {

        if (this.wayPoints != null && currentWayPointIndex < wayPoints.Length)
        {
            if (isTimer)
            {
                this.timer += Time.deltaTime;
            }
            if (timer > this.waitTime)
            {
                this.timer = 0;
                this.isWalking = true;
                animator.SetBool("isWalking", true);
                //Debug.Log("walking..!!");
                angularSpeed = 120;

                nav.SetDestination(wayPoints[currentWayPointIndex].transform.position);
                isTimer = false;
            }
            if (Vector3.Distance(wayPoints[currentWayPointIndex].transform.position, transform.position) < radius)
            {
                currentWayPointIndex++;
                this.isWalking = false;
                animator.SetBool("isWalking", false);
                angularSpeed = 0;
            }
            if (currentWayPointIndex < wayPoints.Length)
            {
                isTimer = true;
            }
            nav.angularSpeed = angularSpeed;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}