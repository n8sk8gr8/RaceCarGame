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
	Vector3 playerStartPosition;
	Quaternion playerOrientation;
	Vector3 aiCarStartPosition;
	Quaternion aiCarOrientation;
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
		playerStartPosition = new Vector3(480, 0.41f, 683.9f);
		playerOrientation = new Quaternion (359, 174, 359, 1);

		aiCarStartPosition = new Vector3 (480, 0.41f, 700);
		aiCarOrientation = new Quaternion (0, 0, 0, 1);
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
		aiCarCompletedRace = false;
		playerCompletedRace = false;
		cars [0].rigidbody.velocity = new Vector3 (0, 0, 0);
		cars [0].rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		cars [0].transform.position = playerStartPosition;
		cars [0].transform.rotation = Quaternion.Euler(playerOrientation.x, playerOrientation.y, playerOrientation.z);

		cars [1].rigidbody.velocity = new Vector3 (0, 0, 0);
		cars [1].rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		cars [1].transform.position = aiCarStartPosition;
		cars [1].transform.rotation = Quaternion.Euler(aiCarOrientation.x, aiCarOrientation.y, aiCarOrientation.z);
		resetGame ();
	}

	void resetGame()
	{
		for(int i = 0; i < allWayPoints.Length; i++)
		{
			checkPoints[i] = false;
			aiCheckPoints[i] = false;
		}
	}
}
