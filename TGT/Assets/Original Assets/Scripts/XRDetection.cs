using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRDetection : MonoBehaviour
{
	public bool isXR = false;
	public GameObject VRPlayerController;
	public Camera UICamera;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach(InputDevice device in devices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        if (!isXR)
        {
        	isXR = true;
        	GameObject PCPlayerController = GameObject.FindGameObjectWithTag("Player");
        	VRPlayerController.SetActive(true);
        	PCPlayerController.SetActive(false);
			GameObject.Find("XRCanvas").GetComponent<Canvas>().worldCamera = UICamera;
        }
    }
}
