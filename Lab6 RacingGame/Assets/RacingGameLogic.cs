using UnityEngine;
using System.Collections;

public class RacingGameLogic : MonoBehaviour {
	public int currentWaypoint;
	public int currentLap;
	public Transform lastWayPoint;
	public GameObject[] cars;
	public GameObject[] allWayPoints;
	// Use this for initialization
	void Start () {
		currentWaypoint = 0;
		currentLap = 0;
		cars = new GameObject[2];
		cars [0] = GameObject.Find ("FrenchClassicCar");
		allWayPoints = new GameObject[10];
		allWayPoints[0]	= GameObject.Find("Waypoint1");
	}
	
	// Update is called once per frame
	void Update () {
		//wayPointTriggered(allWayPoints[0].collider);
	}

	public void wayPointTriggered(Collider waypoint)
	{
//		string waypointTag = waypoint;
	//	Debug.Log (waypointTag);
	}
}
