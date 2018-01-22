using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject eventSystem;
    public GameObject startUI, restartUI, playUI;
    public GameObject startPoint, playPoint, restartPoint;

    // An array to hold the orbs.
    public GameObject[] puzzleSpheres;

    // How many times the orbs light up during the pattern display.
    public int puzzleLength = 5;

    // How many seconds between the orbs light up during the pattern display.
    public float puzzleSpeed = 1f;

    // Variable for storing the order of the pattern display.
    private int[] puzzleOrder;

    // Variable for storing the index during the pattern display.
    private int currentDisplayIndex = 0;

    // Variable for storing the index the player is trying to solve.
    private int currentSolveIndex = 0;

    //Holder for fail and success audio
    public GameObject failAudioHolder;
    public GameObject successAudioHolder;
    public GameObject correctOrbAudioHolder;

    //Variable for warning display for wrong selection of orbs
	public Text m_WarningText;
	public Image m_BackgroundImage;
	private float m_WarningDisplayTime = 0.5f;
	private Renderer rend;
	private bool m_DisplayingWarning;
	public Camera m_Camera;
	public Transform instructions;

    void Start()
    {
        // Update 'player' to be the camera's parent gameobject, i.e. 'GvrEditorEmulator' instead of the camera itself.
        // Required because GVR resets camera position to 0, 0, 0.
        player = player.transform.parent.gameObject;

        // Move the 'player' to the 'startPoint' position.
        player.transform.position = startPoint.transform.position;

        // Set the size of our array to the declared puzzle length.
        puzzleOrder = new int[puzzleLength];

        // Create a random puzzle sequence.
        GeneratePuzzleSequence();
		

    }

    void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
    		// Android close icon or back button tapped.
    		Application.Quit();
  		}
	}

    // Create a random puzzle sequence.
    public void GeneratePuzzleSequence()
    {
        // Variable for storing a random number.
        int randomInt;

        // Loop as many times as the puzzle length.
        for (int i = 0; i < puzzleLength; i++)
        {
            // Generate a random number.
            randomInt = Random.Range(0, puzzleSpheres.Length);

            // Set the current index to the randomly generated number.
            puzzleOrder[i] = randomInt;

        }
    }

    public void EnterDungeon ()
	{
		startUI.SetActive(false);
		// Move the player to the play position.
        iTween.MoveTo(player,
            iTween.Hash(
                "position", playPoint.transform.position,
                "time", 3,
                "easetype", "linear"
            )
        );

      //Make the puzzle spheres non clickable
		for(int i=0; i<puzzleSpheres.Length;i++)
            	puzzleSpheres[i].SetActive(false);
	
	}

    // Begin the puzzle sequence.
    public void StartPuzzle()
    {
    	playUI.SetActive(false);

		//Enable the puzzle orbs
		for(int i=0; i<puzzleSpheres.Length;i++)
            puzzleSpheres[i].SetActive(true);
		
        // Call the DisplayPattern() function repeatedly.
        CancelInvoke("DisplayPattern");
        InvokeRepeating("DisplayPattern", 3, puzzleSpeed);

        // Reset the index the player is trying to solving.
        currentSolveIndex = 0;
    }

    // Reset the puzzle sequence.
    public void ResetPuzzle()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
              
    }

    // Display the pattern
    // Called from StartPuzzle() and invoked repeatingly.
    void DisplayPattern()
    {
        // If we haven't reached the end of the display pattern.
        if (currentDisplayIndex < puzzleOrder.Length)
        {
            Debug.Log("Display index " + currentDisplayIndex + ": Orb index " + puzzleOrder[currentDisplayIndex]);

            // Disable gaze input while displaying the pattern (prevents player from interacting with the orbs).
            eventSystem.SetActive(false);
			
            // Light up the orb at the current index.
            puzzleSpheres[puzzleOrder[currentDisplayIndex]].GetComponent<LightUp>().PatternLightUp(puzzleSpeed);

			// Move one to the next orb.
            currentDisplayIndex++;
        }
        // If we have reached the end of the display pattern.
        else
        {
            Debug.Log("End of puzzle display");

            // Renable gaze input when finished displaying the pattern (allows player to interacte with the orbs).
            eventSystem.SetActive(true);

            // Reset the index tracking the orb being lit up.
            currentDisplayIndex = 0;

            // Stop the pattern display.
            CancelInvoke();
        }
    }

    // Identify the index of the sphere the player selected.
    // Called from LightUp.PlayerSelection() method (see LightUp.cs script).
    public void PlayerSelection(GameObject sphere)
    {
        // Variable for storing the selected index.
        int selectedIndex = 0;

        // Loop throught the array to find the index of the selected sphere.
        for (int i = 0; i < puzzleSpheres.Length; i++)
        {
            // If the passed in sphere is the sphere at the index being checked.
            if (puzzleSpheres[i] == sphere)
            {
                Debug.Log("Looks like we hit sphere: " + i);

                // Update the index of the passed in sphere to be the same as the index being checked.
                selectedIndex = i;
            }
        }

        // Check if the sphere the player selected is correct.
        SolutionCheck(selectedIndex);
    }

    // Check if the sphere the player selected is correct.
    public void SolutionCheck(int playerSelectionIndex)
    {
        // If the sphere the player selected is the correct sphere.
        if (playerSelectionIndex == puzzleOrder[currentSolveIndex])
        {
            Debug.Log("Correct!  You've solved " + currentSolveIndex + " out of " + puzzleLength);
            correctOrbAudioHolder.GetComponent<GvrAudioSource>().Play();
            // Update the tracker to check the next sphere.
            currentSolveIndex++;

            // If this was the last sphere in the pattern display...
            if (currentSolveIndex >= puzzleLength)
            {
            	PuzzleSuccess();
            }
        }
        // If the sphere the player selected is the incorrect sphere.
        else
        {
			PuzzleFailure(playerSelectionIndex);
        }
    }

    // Do this when the player solves the puzzle.
    public void PuzzleSuccess()
    {
    	successAudioHolder.GetComponent<GvrAudioSource>().Play();

        // Enable the restart UI.
        restartUI.SetActive(true);

        // Move the player to the restart position.
        iTween.MoveTo(player,
            iTween.Hash(
                "position", restartPoint.transform.position,
                "time", 2,
                "easetype", "linear"
            )
        );

        //Make the puzzle spheres disappear and nonclickable from outside dungeon
		for(int i=0; i<puzzleSpheres.Length;i++)
            	puzzleSpheres[i].SetActive(false);

		
    }

    // Do this when the player selects the wrong sphere.
	public void PuzzleFailure(int playerSelectionIndex)
    {
        Debug.Log("You failed, resetting puzzle");

        // Get the GVR audio source component on the failAudioHolder and play the audio.
        /* Uncomment the line below during 'A Little More Feedback!' lesson.*/
		
        failAudioHolder.GetComponent<GvrAudioSource>().Play();
		StartCoroutine(DisplayWarning("Wrong! Resetting Puzzle."));

        // Reset the index the player is trying to solving.
        currentSolveIndex = 0;

       	GeneratePuzzleSequence();	
        // Begin the puzzle sequence.
        StartPuzzle();

    }

	private IEnumerator DisplayWarning (string message)
	{			
		// If a warning is already being displayed, quit.
        if(m_DisplayingWarning)
          yield break;
			
		m_DisplayingWarning = true;

		m_WarningText.text = message;
        m_BackgroundImage.enabled = true;

        // Set the position of the warning to that of play UI text position
        m_WarningText.transform.position = instructions.position;
        // Set the rotation of the warning to facing the camera but oriented so it's up is along the global y axis.
		m_WarningText.transform.rotation = Quaternion.LookRotation (m_Camera.transform.forward);
            
       // Wait until the time is up.
       yield return new WaitForSeconds (m_WarningDisplayTime);

       // Remove warning after time is up
       m_WarningText.text = string.Empty;
       m_BackgroundImage.enabled = false;

       // Update statius a warning is no longer being displayed.
       m_DisplayingWarning = false;
	   
	}


}

	
