**Project Name:** Udacity carnival

**Project Objective:** 
Customise the Carnival scene and deploy it to Android device.The goal of this project was basic familiarisation with the Unity editor, learning how to navigate around scene in Unity and deploying the project from Unity to Android/iOS.

**Project Settings:**

  1.Unity 5.6.1p2
  
  2.GoogleVR UnitySDK:1.50.0
      
**Installation on Android:**
This project has been built using Android as the platform.
Install the app by copying the Carnival.apk from the Build directory onto the android device and install by clicking on it.

**Project Task:**
The Udacity Carnival consists of 3 Mini-Games: Plinko, Wheel of Fortune and Coin Toss. With each game, you earn points.The task was to fix the broken Carnival game.
1.Plinko:In this minigame, clicking on the Plinko object, the coin would just drop in a same manner with not much variance to the  oscillation, thus resulting in a score of 200 in most cases.We were supposed to add some variance to the game by changing the oscillation speed and distance so as to achieve some variance to the scores.
2.Wheel of Fortune: We were provided with a wheel which always scored 100 points no matter what.Task was to customise the mini game such that when the wheel lands on smaller wedges, player would score more than when on larger wedges.This was achieved by changing the value against smaller wedges provided in the text input box.The objective of this task was to help us gain familiarity with the Inspector window and how to change properties of objects.
3.Coin Toss : In this mini game, when the user would throw the coin , it would just drop to the ground.We were supposed to change the min and max toss power such that the coin would land on the ground when the power scale is 50%.
4.Aligning the teddy bear to front of the camera so that it doesnot fall on the camera.
5.Personalizing the ScoreBoard to display devloper's name instead of Udacity's Scores i.e. it should show Jagori Samajdar's Scores.

**Extra Features Added**

As a bonus I added firework particle effect and balloons behind the tent to achieve a party atmosphere.

**Challenges:**

1.Figuring out the min and max toss power so that the coin lands on the ground when power scale is up 50%.We had been given a hint to look for a hidden code within the scene to unlock the values.

2.One interesting point to note was the editor preview when the game was played would show mono and not stereo , after some googling I figured that Google VR sdk 1.50.0 , Editor preview would be mono and not stereo but will support simulated headtracking through alt+mouse click.The same showed fine when deployed to  Android phone.

3.This project also helped me to gain more proficiency in Git, GitHub and writing README files.


