using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphUIManager : MonoBehaviour {

    public Text moneyDisplayer;

    private DataController dataController;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
    void Update () {
        moneyDisplayer.text = "Money: " + dataController.money.ToString() + "$";

    }
}
