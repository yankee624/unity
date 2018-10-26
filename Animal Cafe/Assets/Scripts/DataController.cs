using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

    public static DataController instance;

    public List<string> animalNames = new List<string> { "cat","dog","rabbit","tortoise" };

    public long money
    {
        get
        {
            return long.Parse(PlayerPrefs.GetString("money","0"));

        }
        set
        {
            string g = value.ToString();
            PlayerPrefs.SetString("money", g);
        }
    }


    public void SaveAnimal(string animalName, int count, int parttime)
    {
        // if have animal 1, if don't have 0
        PlayerPrefs.SetInt(animalName + "_count", count);
        PlayerPrefs.SetInt(animalName + "_parttime", parttime);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            //PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(gameObject);
            instance = this;

            if (!PlayerPrefs.HasKey("cat_count"))
            {
                SaveAnimal("cat", 1, 0);
            }
        }
    }


}
