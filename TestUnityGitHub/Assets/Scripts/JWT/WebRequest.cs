using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    private string ENDPOINT = "https://jsonplaceholder.typicode.com/posts";
    public Text responseText;

    // Start is called before the first frame update
    void Start()
    {
        Request();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Request()
    {
        WWWForm form = new WWWForm();

        WWW request = new WWW(ENDPOINT);
        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        responseText.text = req.text;
    }
}
