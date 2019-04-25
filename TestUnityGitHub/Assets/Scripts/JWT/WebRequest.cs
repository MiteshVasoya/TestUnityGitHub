using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    private string ENDPOINT = "https://dl-web.wright.edu/lcg/api/data";
    //public Text responseText;

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
            { "sid", sessionID },
        };
        string token = encode(payload);
        //string decodedPayload = decode(token);
        //Debug.Log("decodedPayload: " + decodedPayload);

        // Add a custom header to the request.
        // In this case a basic authentication to access a password protected resource.
        headers["Authorization"] = token;

        WWW request = new WWW(ENDPOINT, rawData, headers);
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

        using (UnityWebRequest www = UnityWebRequest.Post("https://dl-web.wright.edu/lcg/api/session", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("POST successful!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                // Print Headers
                Debug.Log(sb.ToString());

                // Print Body
                Debug.Log(www.downloadHandler.text);

                //sessionID = www.text.ToString();

                //Debug.Log("Session ID:"+sessionID);

                //Request(www.text.ToString());
            }
        }

        
        
    }


}
