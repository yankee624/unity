using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CafeUIManager : MonoBehaviour {

    public Text moneyDisplayer;
    private List<string> animalNames;

    private DataController dataController;

    public GameObject[] cafeWalls;
    public GameObject[] restWalls;

    //동물들 미리 다 만들어놓고 active/deactive 설정하는 방식.
    //private void Start()
    //{
    //    animalNames = DataController.animalNames;

    //    for(int i = 0; i < animalNames.Count; i++)
    //    {
    //        string animalName = animalNames[i];
    //        if(PlayerPrefs.GetInt(animalName + "_count") == 1)
    //        {
    //            GameObject.Find(animalName).SetActive(true);
    //        }
    //        else
    //        {
    //            GameObject.Find(animalName).SetActive(false);
    //        }
    //    }
    //}



    //구매한 동물 object들을 instantiate하는 방식
    [System.Serializable]
    public struct pair
    {
        public string key;
        public GameObject value;
    }
    public pair[] animalButtons;
    public Dictionary<string, GameObject> animalButtonsDict = new Dictionary<string, GameObject>();


    void Start()
    {
        dataController = FindObjectOfType<DataController>();

        animalNames = dataController.animalNames;
        //make dictonary
        for (int i = 0; i < animalButtons.Length; i++)
        {
            animalButtonsDict.Add(animalButtons[i].key, animalButtons[i].value);
        }

        for (int i = 0; i < animalNames.Count; i++)
        {
            string animalName = animalNames[i];
            for (int j=0;j< PlayerPrefs.GetInt(animalName + "_count");j++)
            {

                GameObject animal = Instantiate(animalButtonsDict[animalName]) as GameObject;

                animal.GetComponent<AnimalButton>().animalName = animalName;
                RandomPositioningInsideWalls(animal, cafeWalls, "Canvas");

            }
        }
    }

    // Update is called once per frame
    void Update () {
        moneyDisplayer.text = "Money: "+dataController.money.ToString() + "$";
	}

    public void RandomPositioningInsideWalls(GameObject obj, GameObject[] walls, string parentName)
    {
        //SetParent의 두번째 parameter로 false 넣어야 scale이 제대로 나올때도?
        obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        obj.transform.localPosition = new Vector3(
            Random.Range(walls[0].transform.localPosition.x + 50, walls[1].transform.localPosition.x - 50), Random.Range(walls[2].transform.localPosition.y + 100, walls[3].transform.localPosition.y - 100));

    }

}
