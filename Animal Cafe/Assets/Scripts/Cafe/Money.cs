using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    public int amount;
    private DataController dataController;
    public AnimalButton animalButton;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    public void OnMouseDown()
    {
        amount = animalButton.moneyAmount;
        dataController.money += amount;

        animalButton.canSpawnMoney = true;
        animalButton.moneySpawnTime = animalButton.defaultMoneySpawnTime;

        Destroy(gameObject);
    }
}
