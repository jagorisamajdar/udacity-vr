using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManual : MonoBehaviour {
	public Text title;
	public Text body;
	public Button start;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClick ()
	{
		gameObject.SetActive(false);
		//Change the position of the start button
		start.transform.localPosition = new Vector3(0f,-44.0f, 0f);
		title.text = "Game Rules";
		body.text = "This is a simple point and click game.\n You are trapped inside an abandoned dungeon.\n The only way to escape is \n by repeating an audio visual sequence by clicking on the spheres.\n Have fun!";
	}

}
