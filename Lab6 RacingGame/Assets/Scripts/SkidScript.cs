using UnityEngine;
using System.Collections;

public class SkidScript : MonoBehaviour {
	public float currentFriction;
	float skidAt;
	float skidSoundEmittion;
	float skidDensity;
	public GameObject skidSound;

	// Use this for initialization
	void Start () {
		skidAt = 1.5f;
		skidSoundEmittion = 10f;
		skidDensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		WheelHit hit;
		WheelCollider wheel = (WheelCollider)(collider);
		wheel.GetGroundHit (out hit);
		currentFriction = Mathf.Abs(hit.sidewaysSlip);

		if(skidAt <= currentFriction && skidDensity <= 0)
		{
			Instantiate(skidSound, hit.point, Quaternion.identity);
			skidDensity = 1;
		}
		skidDensity -= skidSoundEmittion * Time.deltaTime;
	}
}
