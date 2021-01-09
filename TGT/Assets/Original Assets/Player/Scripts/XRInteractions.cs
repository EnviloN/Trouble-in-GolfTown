using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class LeftPrimaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class RightPrimaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class LeftSecondaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class RightSecondaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class LeftTriggerButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class RightTriggerButtonEvent : UnityEvent<bool> { }

public class XRInteractions : MonoBehaviour
{
    public LeftPrimaryButtonEvent leftPrimaryButtonPress;
    public RightPrimaryButtonEvent rightPrimaryButtonPress;
    public LeftSecondaryButtonEvent leftSecondaryButtonPress;
    public RightSecondaryButtonEvent rightSecondaryButtonPress;
    public LeftTriggerButtonEvent leftTriggerButtonPress;
    public RightTriggerButtonEvent rightTriggerButtonPress;

    private bool lastLeftPrimaryButtonState = false;
    private bool lastRightPrimaryButtonState = false;
    private bool lastLeftSecondaryButtonState = false;
    private bool lastRightSecondaryButtonState = false;
    private bool lastLeftTriggerButtonState = false;
    private bool lastRightTriggerButtonState = false;

    public List<InputDevice> leftDevices;
    public List<InputDevice> rightDevices;

    private void Awake()
    {
        if (leftPrimaryButtonPress == null) 
        	leftPrimaryButtonPress = new LeftPrimaryButtonEvent();
        if (rightPrimaryButtonPress == null) 
        	rightPrimaryButtonPress = new RightPrimaryButtonEvent();
        if (leftSecondaryButtonPress == null) 
        	leftSecondaryButtonPress = new LeftSecondaryButtonEvent();
        if (rightSecondaryButtonPress == null) 
        	rightSecondaryButtonPress = new RightSecondaryButtonEvent();
        if (leftTriggerButtonPress == null) 
        	leftTriggerButtonPress = new LeftTriggerButtonEvent();
        if (rightTriggerButtonPress == null) 
        	rightTriggerButtonPress = new RightTriggerButtonEvent();

        leftDevices = new List<InputDevice>();
    	rightDevices = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach(InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        leftDevices.Clear();
    	rightDevices.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        if ((device.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
        {
        	if ((device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
        	{
            	leftDevices.Add(device);
        	}
        	if ((device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
        	{
            	rightDevices.Add(device);
        	}
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (leftDevices.Contains(device))
            leftDevices.Remove(device);
        if (rightDevices.Contains(device))
            rightDevices.Remove(device);
    }

    void Update()
    {
        bool tempLeftPrimaryState = false;
        bool tempRightPrimaryState = false;
        bool tempLeftSecondaryState = false;
        bool tempRightSecondaryState = false;
        bool tempLeftTriggerState = false;
        bool tempRightTriggerState = false;

        foreach (var device in leftDevices)
        {
            bool primaryButtonState = false;
            bool secondaryButtonState = false;
            bool triggerButtonState = false;
            tempLeftPrimaryState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState)
                        && primaryButtonState
                        || tempLeftPrimaryState;
            tempLeftSecondaryState = device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState)
                        && secondaryButtonState
                        || tempLeftSecondaryState;
            tempLeftTriggerState = device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState)
                        && triggerButtonState
                        || tempLeftTriggerState;
        }

        foreach (var device in rightDevices)
        {
            bool primaryButtonState = false;
            bool secondaryButtonState = false;
            bool triggerButtonState = false;
            tempRightPrimaryState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState)
                        && primaryButtonState
                        || tempRightPrimaryState;
            tempRightSecondaryState = device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState)
                        && secondaryButtonState
                        || tempRightSecondaryState;
            tempRightTriggerState = device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState)
                        && triggerButtonState
                        || tempRightTriggerState;
        }

        if (tempLeftPrimaryState != lastLeftPrimaryButtonState)
        {
            leftPrimaryButtonPress.Invoke(tempLeftPrimaryState);
            lastLeftPrimaryButtonState = tempLeftPrimaryState;
        }
        if (tempRightPrimaryState != lastRightPrimaryButtonState)
        {
            rightPrimaryButtonPress.Invoke(tempRightPrimaryState);
            lastRightPrimaryButtonState = tempRightPrimaryState;
        }
        if (tempLeftSecondaryState != lastLeftSecondaryButtonState)
        {
            leftSecondaryButtonPress.Invoke(tempLeftSecondaryState);
            lastLeftSecondaryButtonState = tempLeftSecondaryState;
        }
        if (tempRightSecondaryState != lastRightSecondaryButtonState)
        {
            rightSecondaryButtonPress.Invoke(tempRightSecondaryState);
            lastRightSecondaryButtonState = tempRightSecondaryState;
        }
        if (tempLeftTriggerState != lastLeftTriggerButtonState)
        {
            leftTriggerButtonPress.Invoke(tempLeftTriggerState);
            lastLeftTriggerButtonState = tempLeftTriggerState;
        }
        if (tempRightTriggerState != lastRightTriggerButtonState)
        {
            rightTriggerButtonPress.Invoke(tempRightTriggerState);
            lastRightTriggerButtonState = tempRightTriggerState;
        }
    }
}