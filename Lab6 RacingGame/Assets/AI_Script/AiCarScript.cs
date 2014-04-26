using UnityEngine;
using System.Collections;

public class AiCarScript : MonoBehaviour {

	public Transform[] path;
	public Transform pathGroup;
	public Transform[] path_objs;
	public float maxSteer =15.0f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public int currentPathObj;

	// Use this for initialization
	void Start () {
		GetPath ();
	}

	void GetPath(){
		path_objs = pathGroup.GetComponentsInChildren <Transform>();
		path = new Transform[path_objs.Length];
		
		for (int i = 0; i < path_objs.Length; i++) 
		{
			path[i] = path_objs[i];
			Debug.Log(path[i]);
		}


	}

	// Update is called once per frame
	void Update () {
		GetSteer ();
	}

	void GetSteer()
	{
		Vector3 steerVector = transform.InverseTransformPoint (path [currentPathObj].position.x, transform.position.y, path [currentPathObj].position.z);
		float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);
		wheelFL.steerAngle = newSteer;
		wheelFR.steerAngle = newSteer;
	}
}
