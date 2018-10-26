using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabButton : MonoBehaviour {

    public int tabNum;

    private ShopUIManager shopUIManager;

    public void OnClick()
    {
        shopUIManager.ActivateTab(tabNum);
    }

    private void Start()
    {
        shopUIManager = FindObjectOfType<ShopUIManager>();
    }
}
