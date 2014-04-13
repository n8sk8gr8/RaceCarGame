using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	public Transform vechicle;
	float distance;
	float height;
	float rotationDamping;
	float heightDamping;
	float zoomLevel;
	float defaultFieldOfView = 60;
	Vector3 rotationVector;

	// Use this for initialization
	void Start () 
	{
		distance = 5.5f;
		height = 2f;
		rotationDamping = 2.8f;
		heightDamping = 2.2f;
		zoomLevel = 0.7f;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		float targetAngle = rotationVector.y - 175f;
		float targetHeight = vechicle.position.y + height;
		float currentAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		currentAngle = Mathf.LerpAngle (currentAngle, targetAngle, rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp (currentHeight, targetHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler (0, currentAngle, 0);
		transform.position = vechicle.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);


		transform.LookAt (vechicle);
	}

	void FixedUpdate ()
	{
		Vector3 local_V = vechicle.InverseTransformDirection (vechicle.rigidbody.velocity);
		if(local_V.z < 0.4)
		{
			rotationVector.y = vechicle.eulerAngles.y;

		}
		else
		{
			rotationVector.y = vechicle.eulerAngles.y + 180;
		}
			float acceleration = vechicle.rigidbody.velocity.magnitude;
			camera.fieldOfView = defaultFieldOfView + acceleration * zoomLevel;
	}
}
