using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopButton : MonoBehaviour {

    public Button stopButton;

    public Text chosenMultiplierText;
    public Text multiplierText;
    public Text winMoneyText;

    public static float chosenMultiplier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        if (StartGraphGame.running && StartGraphGame.multiplier >= 2)
        {
            chosenMultiplierText.text = multiplierText.text;
            chosenMultiplierText.color = multiplierText.color;
            chosenMultiplier = StartGraphGame.multiplier;

            winMoneyText.text = "+" + (int)(BetButton.betMoney * StopButton.chosenMultiplier) + "$";
            winMoneyText.color = multiplierText.color;

            stopButton.interactable = false;
        }
        


    }



}
