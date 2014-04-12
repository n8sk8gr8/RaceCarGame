using UnityEngine;
using System.Collections;

public class firstCar : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;
	public float maxTorque;
	public float brake;
	// Use this for initialization
	void Start () {
		maxTorque = 50;
		rigidbody.centerOfMass = new Vector3(0, -0.9f, 0);
		brake = 80;
	}

	// Update is called once per frame
	void Update () {
		wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		wheelFR.steerAngle = 10 * Input.GetAxis ("Horizontal");
		wheelFL.steerAngle = 10 * Input.GetAxis ("Horizontal");

		wheelRR.brakeTorque = 0;
		wheelRL.brakeTorque = 0;

		if (Input.GetKey (KeyCode.Space)) 
		{
			wheelRR.brakeTorque = brake;
			wheelRL.brakeTorque = brake;
		}
	}
}
