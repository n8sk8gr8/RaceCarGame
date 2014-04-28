using UnityEngine;
using System.Collections;

public class FrenchClassicCarController : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;
	public float maxTorque;
	public float brake;
	float topSpeed;
	float lowSpeedTurning;
	float highSpeedTurning;
	public bool applyingbrake;
	float maxBrakeTorque;
	float sidewaysFrictionSlip;
	float forwardFrictionSlip;
	public float currentSpeed;
	public float maxSpeed;
	Texture2D speedODiameter;
	Texture2D dial;

	// Use this for initialization
	void Start () {
		maxTorque = 50;
		rigidbody.centerOfMass = new Vector3(0, 0, 0);
		brake = 80;
		topSpeed = 60;
		lowSpeedTurning = 12;
		highSpeedTurning = 1;
		applyingbrake = false;
		maxBrakeTorque = 80;
		maxSpeed = 150;

		sidewaysFrictionSlip = 0.04f;
		forwardFrictionSlip = 0.08f;

	}

	// Update is called once per frame
	void Update () {
		currentSpeed = 2 * Mathf.PI * wheelRL.radius * wheelRL.rpm * 60 / 1000;
		currentSpeed = Mathf.Abs (Mathf.Round(currentSpeed));

		if(currentSpeed < maxSpeed)
		{
			wheelRR.motorTorque = -maxTorque * Input.GetAxis ("Vertical");
			wheelRL.motorTorque = -maxTorque * Input.GetAxis ("Vertical");
		}
		else
		{
			wheelRR.motorTorque = 0;
			wheelRL.motorTorque = 0;
		}

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

		if(rigidbody.velocity.magnitude <  25)
		{
			SetSlip(1, 1);
		}

		// Brake When the spacebar is pressed
		if(Input.GetKey (KeyCode.B))
		{
			wheelRR.brakeTorque = brake;
			wheelRL.brakeTorque = brake;
		}

			HandBrake ();
	}

	void HandBrake ()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			applyingbrake = true;
		}
		else
		{
			applyingbrake = false;
		}
		if(applyingbrake)
		{
			wheelRR.brakeTorque  = maxBrakeTorque;
			wheelRL.brakeTorque  = maxBrakeTorque;

			wheelRR.motorTorque = 0;
			wheelRL.motorTorque = 0;
			SetSlip(forwardFrictionSlip, sidewaysFrictionSlip);
		}
		else
		{
			SetSlip (1, 1);
		}
	}

	void SetSlip(float vechicleForwardFriction, float vechicleSidewaysFriction)
	{
		WheelFrictionCurve temp;
		temp = wheelRR.forwardFriction;
		temp.stiffness = vechicleForwardFriction;
		wheelRR.forwardFriction = temp;

		temp = wheelRL.forwardFriction;
		temp.stiffness = vechicleForwardFriction;
		wheelRL.forwardFriction = temp;

		temp = wheelFR.forwardFriction;
		temp.stiffness = vechicleForwardFriction;
		wheelFR.forwardFriction = temp;

		temp = wheelFL.forwardFriction;
		temp.stiffness = vechicleForwardFriction;
		wheelFL.forwardFriction = temp;


		temp = wheelRR.sidewaysFriction;
		temp.stiffness = vechicleSidewaysFriction;
		wheelRR.sidewaysFriction = temp;

		temp = wheelRL.sidewaysFriction;
		temp.stiffness = vechicleSidewaysFriction;
		wheelRL.sidewaysFriction = temp;

		temp = wheelFR.sidewaysFriction;
		temp.stiffness = vechicleSidewaysFriction;
		wheelFR.sidewaysFriction = temp;

		temp = wheelFL.sidewaysFriction;
		temp.stiffness = vechicleSidewaysFriction;
		wheelFL.sidewaysFriction = temp;

	}
}
