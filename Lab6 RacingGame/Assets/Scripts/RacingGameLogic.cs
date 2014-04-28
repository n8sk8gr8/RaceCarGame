using UnityEngine;
using System.Collections; 

public class RacingGameLogic : MonoBehaviour {
	public int currentWaypoint;
	public int currentLap;
	public int numberOfLabs;
	public GameObject[] cars;
	public GameObject[] allWayPoints;
	public bool[] checkPoints;
	public bool[] aiCheckPoints;
	public bool playerCompletedRace;
	public bool aiCarCompletedRace;
	string checkWho;
	string winOrLose;
	
	// Use this for initialization
	void Start () {
		currentLap = 0;
		numberOfLabs = 1;
		playerCompletedRace = false;
		aiCarCompletedRace = false;
		cars = new GameObject[2];
		cars [0] = GameObject.Find ("FrenchClassicCar");
		cars[1] = GameObject.Find ("RivalCar");
		allWayPoints = new GameObject[10];
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			allWayPoints[i]	= GameObject.Find("Waypoint" + (i + 1));
		}
		checkPoints = new bool[10];
		aiCheckPoints = new bool[10];
		winOrLose = "";
		checkWho = "";
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
			aiCheckPoints[position] = true;
		}
		checkWho = "Player";
		raceWon ();

		if(distance < 10)
		{
			checkPoints[position] = true;
		}
		checkWho = "Ai";
		raceWon ();
	}

	public void raceWon()
	{
		if(checkWho == "Player")
		{
			foreach(bool checkPoint in checkPoints)
			{
				if(checkPoint == false)
				{
					return;
				}
			}
			playerCompletedRace = true;
			winOrLose = "WON";
			OnGUI ();
		}
		if(checkWho == "Ai")
		{
		foreach(bool checkPoint in aiCheckPoints)
			{
				if(checkPoint == false)
				{
					return;
				}
			}
			aiCarCompletedRace = true;
			winOrLose = "Lost";
			OnGUI ();
		}
	}

	void OnGUI()
	{
		if(playerCompletedRace == true || aiCarCompletedRace == true)
		{
			GUI.Box (new Rect (0, 0,  Screen.width, Screen.height), "YOU " + winOrLose + " THE RACE!!!!");
			if(GUI.Button (new Rect (Screen.width/4, Screen.height * 3/4, 80, 20), "Play Again?"))
			{
				playAgain();
			}

			if(GUI.Button (new Rect (Screen.width * 3/4, Screen.height * 3/4, 80, 20), "Quit"))
			{
				Application.Quit();
			}
		}
	}

	void playAgain()
	{
		Application.LoadLevel (1);
	}

	public bool playerWon()
	{
		return playerCompletedRace;
	}

	public bool computerWon()
	{
		return aiCarCompletedRace;
	}
}
