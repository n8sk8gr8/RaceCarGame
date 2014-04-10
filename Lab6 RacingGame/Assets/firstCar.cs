using UnityEngine;
using System.Collections;

public class firstCar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate (0, 0, -10f * Time.deltaTime);
			
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate (0, 0, 10f * Time.deltaTime);

		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate (10f * Time.deltaTime, 0, 0);
			
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate (-10f * Time.deltaTime, 0, 0);
			
		}
	}
}
