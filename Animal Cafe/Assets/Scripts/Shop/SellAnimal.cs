using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellAnimal : MonoBehaviour {

    public string animalName;
    public int sellPrice;
    public int count;
    public Text countDisplayer;
    private DataController dataController;

    // Use this for initialization
    public void Sell()
    {
        count = PlayerPrefs.GetInt(animalName + "_count");
        if (count >= 1)
        {
            PlayerPrefs.SetInt(animalName + "_count", count - 1);
            count -= 1;
            dataController.money += sellPrice;
        }
    }

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    private void Update()
    {
        count = PlayerPrefs.GetInt(animalName + "_count");
        countDisplayer.text = string.Format("{0}(보유:{1})", animalName, count);
    }

}
