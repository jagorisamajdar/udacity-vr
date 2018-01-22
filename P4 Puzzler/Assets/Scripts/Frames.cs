using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frames : MonoBehaviour {
	public Text myText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float fps = (1.0f / Time.smoothDeltaTime);
		myText.text =  fps.ToString();
	}
}
