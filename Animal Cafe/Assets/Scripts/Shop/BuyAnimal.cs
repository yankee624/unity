using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAnimal : MonoBehaviour {

    public string animalName;
    public int buyPrice;
    public int count;
    public Text countDisplayer;
    public DataController dataController;

    public void Buy()
    {
        if (dataController.money >= buyPrice)
        {
            PlayerPrefs.SetInt(animalName + "_count", count+1);
            dataController.money -= buyPrice;
            count += 1;
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
