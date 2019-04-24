using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public GameObject player;
    private GameObject currentActivePlayer;
    public GameObject[] wayPoints;
    private PlayerAction playerAction;
    public Transform[] spawnPoints;
    public Transform chairSpwanPoint;
    public Transform chair;

    // Start is called before the first frame update
    void Start()
    {
        currentActivePlayer = player;
        playerAction = currentActivePlayer.GetComponent<PlayerAction>();
        Debug.Log("PlayerAction started");

        StartCoroutine(startWalking());

        //StartCoroutine(spawn(spawnPoints[0]));
        //StartCoroutine(loadChair());
    }

    int count = 0;

    // Update is called once per frame
    void Update()
    {
        //count++;

        //if(count == 500)
            //StartCoroutine(spawn(spawnPoints[1]));
    }

    private IEnumerator startWalking()
    {
        yield return new WaitForSeconds(5f);
        playerAction.startWalkingWithWaiting(wayPoints, 2);
    }

    private IEnumerator spawn(Transform spawnPoint)
    {
        GameObject temp = currentActivePlayer;
        yield return new WaitForSeconds(3f);
        currentActivePlayer = Instantiate(currentActivePlayer, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(temp);
    }

    private IEnumerator loadChair()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(chair, chairSpwanPoint.position, chairSpwanPoint.rotation);
    }
}
