using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class AnimalInfo
{
    public string name;
    public int count;
    public bool parttime;


}

public class Test : MonoBehaviour {
    //public Dictionary<string, object> dicData = new Dictionary<string, object>();
    public string jsonData = null;
    public List<AnimalInfo> animalInfoList = new List<AnimalInfo>();


	// Use this for initialization
	void Start () {
        //Dictionary<string, object> tempData = new Dictionary<string, object>();
        List<AnimalInfo> temp = new List<AnimalInfo>();
        //tempData.Add("name", "cat");
        //tempData.Add("count", 20);
        //tempData.Add("parttime", true);
        AnimalInfo tempanim = new AnimalInfo();
        tempanim.name = "dog";
        temp.Add(tempanim);
        tempanim.name = "cat";
        temp.Add(tempanim);

        jsonData = JsonFx.Json.JsonWriter.Serialize(temp);
        File.WriteAllText(Application.dataPath+"/jsonData.json", jsonData);
        Debug.Log(jsonData);

        string s = File.ReadAllText(Application.dataPath + "/jsonData.json");
        Debug.Log(s);
        animalInfoList = JsonFx.Json.JsonReader.Deserialize<List<AnimalInfo>>(s);
        //dicData = JsonFx.Json.JsonReader.Deserialize<Dictionary<string, object>>(s);
        //Debug.Log(dicData);
        //Debug.Log(dicData["name"]+"  "+dicData["count"]+"  "+dicData["parttime"]);
        Debug.Log(animalInfoList);
        Debug.Log(animalInfoList[0].name);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
