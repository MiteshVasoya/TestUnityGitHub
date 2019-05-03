using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl2 : MonoBehaviour
{

    private bool gyroEnabled;
    private Gyroscope gyroscope;

    private GameObject cameraContainer;
    private Quaternion rot;

    public GameObject camera;
    public GameObject gyroscopeGo;

    // Start is called before the first frame update
    void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.SetParent(camera.transform.parent);
        cameraContainer.transform.position = camera.transform.position;
        camera.transform.SetParent(cameraContainer.transform);

        EnableGyro();
    }

    public void EnableGyro()
    {
        Debug.Log("gyroEnabled: " + gyroEnabled);
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            //cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, -90f);
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
        if (gyroEnabled)
        {
            //gyroscopeGo.transform.localRotation = gyroscope.attitude * rot;
            cameraContainer.transform.Rotate(0, -gyroscope.rotationRateUnbiased.y, 0);
            camera.transform.Rotate(-gyroscope.rotationRateUnbiased.x, 0, 0);
        }
    }

    private void LateUpdate()
    {
        if(gyroEnabled)
        {
            Debug.Log("gyroscope attitude: " + gyroscope.attitude);
        }
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
