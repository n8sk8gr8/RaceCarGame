﻿using UnityEngine;
using System.Collections;

public class AiCarScript : MonoBehaviour {

	public Vector3 centerOfMass;
	public Transform[] path;
	public Transform pathGroup;
	public Transform[] path_objs;
	public float maxSteer = 22.0f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public int currentPathObj;
	public float distFromPath = 10.0f;
	public float maxTorque = 50;
	public float currentSpeed;
	public float topSpeed = 150.0f;
	public float decellarationSpeed =10.0f;

	// Use this for initialization
	void Start () {
		rigidbody.centerOfMass = centerOfMass;
		GetPath ();
	}

	void GetPath(){
		path_objs = pathGroup.GetComponentsInChildren <Transform>();
		path = new Transform[path_objs.Length];
		
		for (int i = 0; i < path_objs.Length; i++) 
		{

			path[i] = path_objs[i];

		}
	}

	// Update is called once per frame
	void Update () {
		GetSteer ();
		Move ();
	}

	void GetSteer()
	{
		Vector3 steerVector = transform.InverseTransformPoint (path [currentPathObj].position.x, transform.position.y, path [currentPathObj].position.z);
		float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);
		wheelFL.steerAngle = newSteer;
		wheelFR.steerAngle = newSteer;

		if (steerVector.magnitude <= distFromPath) 
		{
					//Debug.Log(currentPathObj);
						currentPathObj++;
		}
		if (currentPathObj >= path.Length) 
		{
						currentPathObj = 0;
		}
	}

	void Move()
	{
		currentSpeed = 2*(22/7)*wheelRL.radius*wheelRL.rpm * 60 / 1000;
		currentSpeed = Mathf.Round (currentSpeed);  
		if (currentSpeed <= topSpeed) 
		{ 
						wheelRL.motorTorque = maxTorque * 0.7f;
						wheelRR.motorTorque = maxTorque * 0.7f;
						wheelRL.brakeTorque = 0;  
						wheelRR.brakeTorque = 0;  
		} 
		else 
		{
			wheelRL.motorTorque=0;
			wheelRR.motorTorque=0;
			wheelRL.brakeTorque = decellarationSpeed;  
			wheelRR.brakeTorque = decellarationSpeed; 
		}
	}
}
