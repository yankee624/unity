using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour {


    public static int betMoney=0;
    public int addAmount;

    public Text betText;

    public DataController dataController;

    public GameObject notEnoughMoneyPanel;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    public void AddBet()
    {
        if (!StartGraphGame.running)
        {
            if (dataController.money >= betMoney + addAmount)
            {
                betMoney += addAmount;
                betText.text = "Bet: " + betMoney + "$";
            }
            else
            {
                notEnoughMoneyPanel.SetActive(true);
            }
        }
    }

    public void ResetBet()
    {
        if (!StartGraphGame.running)
        {
            betMoney = 0;
            betText.text = "Bet: " + betMoney + "$";
        } 
    }


}
