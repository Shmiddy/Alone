﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

    // text object that will be modified to show the ending information
    private Text displayText;

    private float delay = 8; // 8 second delay
    private float nextUsage; // used to track the next time to use the delay

    private float moveAmount = 0.0125f; // how much to move the text per frame when it is moving
    private float fadeSpeed = 0.0041f;  // this amount is the the max alpha value (1) divided by the delay (seconds) multiplied by the number of frames (30fps)
                                        // so it is 1 / (8*30)   or 1maxAlpha / (8secondFadeTime * 30fps)

    private bool fadingIn = true; // used to track whether to fade in or fade out the text
    private int textStep = 0; // int used to track what message to display when each one scrolls by

	// Use this for initialization
	void Start () {

        // set up the initial delay (the current time plus 2 seconds)
        nextUsage = Time.time + delay;

        // find the text object so I don't have to set it manually
        displayText = GameObject.Find("DisplayText").GetComponent<Text>();

        // initially make the text have 0 opacity
        Color tempColor = new Color(255, 255, 255, 0);
        displayText.color = tempColor;

        // the initial text that appears
        // use this line once the full setup with other scenes is working
        if(textStep == 0)
        {
            //displayText.text = MainManager.days + " days have finally passed.";
            displayText.text = 3 + " days have finally passed.";
        }
        


	}
	
	// Update is called once per frame
	void Update () {

        FadeText();
        // always move the text down
        MoveTextDown();

        // after 2 seconds have passed, do this block
        if(Time.time > nextUsage)
        {
            // set the next delay time (now + delay seconds)
            nextUsage = Time.time + delay;
            
            //update the text to be displayed
            textStep++;
            if(textStep == 1)
            {
                //displayText.text = "In this time, I collected " + MainManager.storedFoodNames.Count + " pounds of food to survive for "
                //+ (int)(MainManager.storedFoodNames.Count / 6) + " months."
                displayText.text = "In this time, I collected " + 12 + " pounds of food to survive for " + (int)(13 / 6) + " months.";
            } 
            // block that checks progress of the raft or what exaclty was used to esapce the town
            else if (textStep == 2)
            {
                displayText.text = "The raft was just barely built in time, and I made it downriver.";
            }
            // final message block to determine if you lived or not. Stops the movement upon hitting this block
            else
            {
                displayText.text = "With these supplies, I was able to escape.";
            }
            // can add more text steps here for different end messages


            // move the block of text back up
            ResetHeight();
            // set the text to be 0 opacity again
            Color tempColor = new Color(255, 255, 255, 0);
            displayText.color = tempColor;
            // let the text fade back in (from 0 to 1)
            fadingIn = true;

        }

        // check the opacity to figure out whether to fade in our out
        if (displayText.color.a <= 0.0f)
        {
            fadingIn = true;
        }
        else if (displayText.color.a >= 1.1f)
        {
            fadingIn = false;
        }
	}

    // fade the text by a certain amount
    void FadeText()
    {
        // have to use a temp color because you cannot directly change the alpha value
        Color tempColor = displayText.color;

        // if the text is fading in, increase opacity
        if(fadingIn)
        {
            tempColor.a += fadeSpeed;
        }
        // otherwise, decrease opacity
        else
        {
            tempColor.a -= fadeSpeed;
        }        

        displayText.color = tempColor;
    }

    // method to move text down by moveAmount
    void MoveTextDown()
    {
        Vector3 tempPos = displayText.rectTransform.position;
        tempPos.y -= moveAmount;
        displayText.rectTransform.position = tempPos;
    }

    // method to reset the text height near the top of the screen
    void ResetHeight()
    {
        Vector3 tempPos = displayText.rectTransform.position;
        tempPos.y = 2.0f;
        displayText.rectTransform.position = tempPos;
    }
}
