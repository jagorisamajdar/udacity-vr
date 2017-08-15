using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked() 
	private bool locked = true;
    // Create a boolean value called "opening" that can be checked in Update() 
	private bool opening=false;

	public AudioClip[] audioClips;
	private AudioSource audioSource;

    void Update() {
        // If the door is opening and it is not fully raised
            // Animate the door raising up
		if(opening){
			transform.Translate (0, 2.5f * Time.deltaTime, 0, Space.World);
			audioSource = GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioClips[1]);
		}
    }

    public void OnDoorClicked() {
     
		// If the door is clicked and unlocked
		// Set the "opening" boolean to true
		if (locked==false) {
			opening = true;

		}
		else {

			audioSource = GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioClips[0]);
		}
		        
    }

    public void Unlock()
    {
        // You'll need to set "locked" to false here
		if (Key.keyCollected) {
			locked = false;
		}
    }
}
