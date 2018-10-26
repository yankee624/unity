using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour {

    public Text moneyDisplayer;

    private DataController dataController;

    public int totalTabNum = 3;
    public GameObject[] tabs;
    public GameObject[] contents;

    public Sprite activeButtonImage;
    public Sprite inactiveButtonImage;

    public void ActivateTab(int tabNum)
    {
        for(int i = 0; i < totalTabNum; i++)
        {
            if (i == tabNum)
            {
                tabs[i].GetComponent<Image>().sprite = activeButtonImage;
                contents[i].SetActive(true);
            }
            else
            {
                tabs[i].GetComponent<Image>().sprite = inactiveButtonImage;
                contents[i].SetActive(false);
            }
        }
    }


    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    private void Update()
    {
        moneyDisplayer.text = "Money: " + dataController.money.ToString() + "$";
    }
}
