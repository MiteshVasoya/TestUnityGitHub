using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    private string ENDPOINT = "https://jsonplaceholder.typicode.com/posts";
    //public Text responseText;

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

        var payload = new Dictionary<string, object>()
        {
            { "claim1", 0 },
            { "claim2", "claim2-value" }
        };
        string token = encode(payload);
        string decodedPayload = decode(token);

        WWW request = new WWW(ENDPOINT);
        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        //responseText.text = req.text;
        Debug.Log("Response: "+ req.text);
    }

    private string encode(Dictionary<string, object> payload)
    {
        var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
        Debug.Log("JWT: " + token);

        return token;
    }

    private string decode(string token)
    {
        var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        try
        {
            string jsonPayload = JWT.JsonWebToken.Decode(token, secretKey);
            Debug.Log("jsonPayload: "+jsonPayload);
            return jsonPayload;
        }
        catch (JWT.SignatureVerificationException)
        {
            Debug.Log("Invalid token!");
        }
        return null;
    }
}
