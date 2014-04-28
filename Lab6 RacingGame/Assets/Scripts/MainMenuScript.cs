using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public GameObject startGame;
	public GameObject quitGame;

	// Use this for initialization
	void Start () {
		startGame = GameObject.Find("Start Game");
		quitGame = GameObject.Find("Quit Game");
	}
	void OnMouseEnter()
	{
		renderer.material.color = Color.black;
	}

	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}

	void OnMouseUp()
	{
		string clickedText = gameObject.name;
		if(clickedText == startGame.name)
		{
			Debug.Log ("Start Game Pressed");
			Application.LoadLevel(1);
		}
		else if(clickedText == quitGame.name)
		{
			Debug.Log ("Quit Game Pressed");
			Application.Quit ();
		}
	}
}
