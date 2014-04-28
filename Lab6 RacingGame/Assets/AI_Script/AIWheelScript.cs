using UnityEngine;
using System.Collections;

public class AIWheelScript : MonoBehaviour {

	public WheelCollider myWheelCollider;
	private Vector3 wheel;
	// Use this for initialization
	void Start () {
		wheel = new Vector3 (0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(myWheelCollider.rpm*360*Time.deltaTime,0,0);
		wheel.y  = myWheelCollider.steerAngle - transform.localEulerAngles.z; 
		transform.localEulerAngles = new Vector3(0 ,wheel.y, 0);
		//RaycastHit hit;
		//Vector3 wheelPos;
//		if (Physics.Raycast(myWheelCollider.transform.position,-myWheelCollider.transform.up,hit,myWheelCollider.radius + myWheelCollider.suspensionDistance))  
//			wheelPos = hit.point + myWheelCollider.transform.up * myWheelCollider.radius;  
//		else   
//			wheelPos = myWheelCollider.transform.position - myWheelCollider.transform.up * myWheelCollider.suspensionDistance;  
		
//		transform.position = wheelPos ;  

	}
}
