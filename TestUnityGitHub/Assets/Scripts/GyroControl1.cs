using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl1 : MonoBehaviour
{

    private bool gyroEnabled;
    private Gyroscope gyroscope;

    private GameObject cameraContainer;
    private Quaternion rot;

    public GameObject camera;

    public TextMesh statusText;

    public float sensitivityZ = 5;

    public GameObject eyes;

    private float rotationX;
    private float rotationY;

    public GameObject gyroScopeGO;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("-----GyroControl Start-----");
        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.SetParent(camera.transform.parent);
        //cameraContainer.transform.position = camera.transform.position;
        //camera.transform.SetParent(cameraContainer.transform);

        //initialize your x & y angles to match the camera's initial orientation.
        gyroScopeGO.transform.eulerAngles = eyes.transform.eulerAngles;

        EnableGyro();
    }

    public void EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            //cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            //rot = new Quaternion(0, 0, 1, 0);

            gyroEnabled = true;
        }
        else
        {
            gyroEnabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //statusText.text = camera.transform.localEulerAngles.x.ToString() + ", " + camera.transform.localEulerAngles.y.ToString();

        //statusText.text = "Eyes: " + eyes.transform.eulerAngles.ToString();
        //statusText.text += "\nCamera: " + camera.transform.eulerAngles.ToString();
        //statusText.text += "\ndelta X:" + Math.Abs(eyes.transform.eulerAngles.x - camera.transform.eulerAngles.x) + ", delta Y:" + Math.Abs(eyes.transform.eulerAngles.y - camera.transform.eulerAngles.y);

        //statusText.text = "Eye:" + eyes.transform.eulerAngles.ToString();
        //statusText.text += "\nCamera: " + camera.transform.eulerAngles.ToString();
        if (gyroEnabled)
        {
            gyroScopeGO.transform.Rotate(-gyroscope.rotationRateUnbiased.x, -gyroscope.rotationRateUnbiased.y, -gyroscope.rotationRateUnbiased.z);

            //Limit the Camera movements
            rotationX = (Mathf.Abs(gyroScopeGO.transform.eulerAngles.x - eyes.transform.eulerAngles.x) > 45f) ? 0 : -gyroscope.rotationRateUnbiased.x;
            rotationY = (Mathf.Abs(gyroScopeGO.transform.eulerAngles.y - eyes.transform.eulerAngles.y) > 45f) ? 0 : -gyroscope.rotationRateUnbiased.y;

            statusText.text = "rotationX:" + rotationX + ", rotationY:"+ rotationY;
            statusText.text += "\ncamera lR:" + camera.transform.localRotation;
            statusText.text += "\ngyroScopeGO lR:" + gyroScopeGO.transform.localRotation;

            //camera.transform.eulerAngles = new Vector3(pitch, yaw, 0);
            camera.transform.Rotate(rotationX, rotationY, 0);
            
        }
    }

    void LockedRotation()
    {
        float rotationZ = camera.transform.localEulerAngles.z;
        rotationZ += Input.GetAxis("Mouse ScrollWheel") * sensitivityZ * 10;
        rotationZ = Mathf.Clamp(rotationZ, -45.0f, 45.0f);

        camera.transform.localEulerAngles = new Vector3(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, -rotationZ);
    }

    public void DisableGyro()
    {
        gyroEnabled = false;
    }

    public static string GetGameObjectPath(GameObject obj)
    {
        string path = "/"; //+ obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

}
