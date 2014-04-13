using UnityEngine;
using System.Collections;

public class firstCar : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;
	public float maxTorque;
	public float brake;
	float topSpeed;
	float lowSpeedTurning;
	float highSpeedTurning;
	// Use this for initialization
	void Start () {
		maxTorque = 50;
		rigidbody.centerOfMass = new Vector3(0, 0, 0);
		brake = 80;
		topSpeed = 60;
		lowSpeedTurning = 12;
		highSpeedTurning = 1;
	}

	// Update is called once per frame
	void Update () {
		wheelRR.motorTorque = -maxTorque * Input.GetAxis ("Vertical");
		wheelRL.motorTorque = -maxTorque * Input.GetAxis ("Vertical");

		float speed = rigidbody.velocity.magnitude / topSpeed;
		float currentSteeringAngle = Mathf.Lerp (lowSpeedTurning, highSpeedTurning, speed);
		currentSteeringAngle *= Input.GetAxis ("Horizontal");
		wheelFR.steerAngle = currentSteeringAngle;
		wheelFL.steerAngle = currentSteeringAngle;

		if(Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey (KeyCode.DownArrow) == false)
		{
			wheelRL.brakeTorque = 25;
			wheelRR.brakeTorque = 25;
		}
		else
		{
			wheelRR.brakeTorque = 0;
			wheelRL.brakeTorque = 0;
		}

		// Brake When the spacebar is pressed
		if (Input.GetKey (KeyCode.Space)) 
		{
			wheelRR.brakeTorque = brake;
			wheelRL.brakeTorque = brake;
		}
	}
}
