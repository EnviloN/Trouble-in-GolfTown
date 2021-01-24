using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class XRReadyEvent : UnityEvent<bool> { }

public class XRDetection : MonoBehaviour
{
	public bool isXR;
	public GameObject XRPlayerController;
    public GameObject XRDialogCanvas;
	public Camera UICamera;
	public XRReadyEvent XRReady;

    private void Awake()
    {
    	isXR = false;
    	XRReady = new XRReadyEvent();
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
        	GameObject PCPlayerController = GameObject.FindGameObjectWithTag("Player");
        	XRPlayerController.SetActive(true);
        	PCPlayerController.SetActive(false);
            XRDialogCanvas.SetActive(true);
			GameObject.Find("XRCanvas").GetComponent<Canvas>().worldCamera = UICamera;
            GameObject.Find("Help").SetActive(false);
			XRReady.Invoke(true);
        	isXR = true;
        }
    }
}
