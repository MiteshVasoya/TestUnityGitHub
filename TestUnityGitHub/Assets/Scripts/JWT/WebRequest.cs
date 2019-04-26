using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    private string ENDPOINT = "https://dl-web.wright.edu/lcg/api/data";
    public Text responseText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadWebSession());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Request(string sessionID)
    {
        WWWForm form = new WWWForm();
        form.AddField("q1", 4);
        form.AddField("q2", 2);
        form.AddField("q3", 1);
        form.AddField("q4", 5);

        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

        var payload = new Dictionary<string, object>()
        {
            { "sid", sessionID.ToString() },
        };
        string token = encode(sessionID.ToString());
        string decodedPayload = decode(token);
        Debug.Log("decodedPayload: " + decodedPayload);

        // Add a custom header to the request.
        // In this case a basic authentication to access a password protected resource.
        headers["Authorization"] = token;

        WWW request = new WWW(ENDPOINT, rawData, headers);

        Debug.Log("Request: www request: " + request);

        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        responseText.text = req.text;
        Debug.Log("Response: "+ req.text);
    }
    
    private string encode(object payload)
    {
        var secretKey = "socialdeterminants";
        string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
        Debug.Log("JWT: " + token);

        return token;
    }

    private string decode(string token)
    {
        var secretKey = "socialdeterminants";
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

    IEnumerator LoadWebSession()
    {
        Debug.Log("Device ID: " + SystemInfo.deviceUniqueIdentifier);

        WWWForm form = new WWWForm();
        form.AddField("did", SystemInfo.deviceUniqueIdentifier);

        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

        WWW www = new WWW("https://dl-web.wright.edu/lcg/api/session", rawData, headers);
        yield return www;
        Debug.Log("POST successful!");

        // Print Body
        Debug.Log(www.text);
        responseText.text = www.text;

        Dictionary<string, object> sessionResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(www.text);
        Request((string)sessionResponse["sid"]);
        
    }


}
