//	FaceObjectToCamera.cs 
//	original (CameraFacing.cs) by Neil Carter (NCarter)


using UnityEngine;
using System.Collections;

public class FaceObejctToCamera : MonoBehaviour
{
	public Camera referenceCamera;

	void Awake() {
		// if no camera referenced, grab the main camera
		if (!referenceCamera)
			referenceCamera = Camera.main;
	}

	void Update() {
		transform.LookAt(referenceCamera.transform);
	}
}