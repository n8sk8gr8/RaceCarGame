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
		currentLap = 0;
		numberOfLabs = 1;
		raceCompleted = false;
		cars = new GameObject[2];
		cars [0] = GameObject.Find ("FrenchClassicCar");
		cars[1] = GameObject.Find ("RivalCar");
		allWayPoints = new GameObject[10];
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			allWayPoints[i]	= GameObject.Find("Waypoint" + (i + 1));
		}
		checkPoints = new bool[10];
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			wayPointTriggered(allWayPoints[i].collider, i);
		}
	}

	public void wayPointTriggered(Collider waypoint, int position)
	{
		float distance = (cars [0].transform.position - waypoint.transform.position).magnitude;
		float ai_Car_distance = (cars [1].transform.position - waypoint.transform.position).magnitude;

		if(ai_Car_distance < 10)
		{
			checkPoints[position] = true;
		}
		raceWon ();

		if(distance < 10)
		{
			checkPoints[position] = true;
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
