using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DeathCounter : MonoBehaviour
{
    public DeathCount count;
    private String path;
    public Text uiCountDeath;

    private void Start()
    {
        count = new DeathCount();
        uiCountDeath = GameObject.Find("UIcountDeath").GetComponent<Text>();

        //path = Path.Combine(Application.dataPath, "Death.json");
        path = Path.Combine(Application.persistentDataPath, "Death.json");
        if(File.Exists(path))
        {
            count = JsonUtility.FromJson<DeathCount>(File.ReadAllText(path));
            UpdateUI();
        }else{uiCountDeath.text = "0";}
    }

    public void UpdateUI()
    {
        uiCountDeath.text = count.countDeath.ToString();
        WriteDownCount();
    }

    public void WriteDownCount()
    {
        File.WriteAllText(path, JsonUtility.ToJson(count));
    }
}

[Serializable]
public class DeathCount
{
    public int countDeath;
}
