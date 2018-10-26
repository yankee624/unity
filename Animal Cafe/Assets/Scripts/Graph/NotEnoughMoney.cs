using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughMoney : MonoBehaviour {

    public GameObject notEnoughMoneyPanel;

    public void OKButton()
    {
        notEnoughMoneyPanel.SetActive(false);
    }

}
