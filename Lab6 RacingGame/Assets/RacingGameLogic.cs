using UnityEngine;
using System.Collections;

public class RacingGameLogic : MonoBehaviour {
	public int currentWaypoint;
	public int currentLap;
	public int numberOfLabs;
	public GameObject[] cars;
	public GameObject[] allWayPoints;
	public bool[] checkPoints;
	public bool raceCompleted;
	// Use this for initialization
	void Start () {
		//currentWaypoint = 0;
		currentLap = 0;
		numberOfLabs = 1;
		raceCompleted = false;
		cars = new GameObject[2];
		cars [0] = GameObject.Find ("FrenchClassicCar");
		allWayPoints = new GameObject[10];
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			allWayPoints[i]	= GameObject.Find("Waypoint" + (i + 1));
		}
		//allWayPoints[0]	= GameObject.Find("Waypoint1");
		//allWayPoints[1]	= GameObject.Find("Waypoint1");
		checkPoints = new bool[10];
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			wayPointTriggered(allWayPoints[i].collider, i);
		}
		//wayPointTriggered(allWayPoints[0].collider);
	}

	public void wayPointTriggered(Collider waypoint, int position)
	{
		float distance = (cars [0].transform.position - waypoint.transform.position).magnitude;

		if((cars[0].transform.position - waypoint.transform.position).magnitude < 10)
		{
			checkPoints[position] = true;
			//Debug.Log (distance);
		}
		raceWon ();
	}

	public void raceWon()
	{
		foreach(bool checkPoint in checkPoints)
		{
			if(checkPoint == false)
			{
				return;
			}
		}
		raceCompleted = true;
		Debug.LogWarning ("YOU WON THE RACE!!!!");
	}
}
