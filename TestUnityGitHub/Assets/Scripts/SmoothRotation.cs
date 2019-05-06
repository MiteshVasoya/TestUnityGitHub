using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    public Transform[] targets;
    private Transform target;
    public float speed = 0.3F;

    private void Start()
    {
        StartCoroutine(story());
    }

    public IEnumerator story()
    {
        smoothlyRotate(targets[0]);
        yield return new WaitForSeconds(5f);

        smoothlyRotate(targets[1]);
        yield return new WaitForSeconds(5f);

        smoothlyRotate(targets[2]);
    }

    public void smoothlyRotate(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target != null)
        {
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

            Debug.Log("Angle: "+ Vector3.Angle((target.transform.position - transform.position), gameObject.transform.forward));

            if (Vector3.Angle((target.transform.position - transform.position), gameObject.transform.forward) < 0.01)
                target = null;
        }
    }
}
