using System;
using System.IO;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    TimeData timeData;
    [SerializeField]
    Block blockRef;
    long timeDiff;
    DateTime diffInterval;


    [SerializeField]
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diffInterval = new DateTime();
        diffInterval = diffInterval.AddSeconds(2);

        timeData = new TimeData();

       LoadTimeData();

        timeData.startTime = DateTime.Now.Ticks;

        if (timeData.closeTime != 0)
        {
            timeDiff = timeData.startTime - timeData.closeTime;
        }

        DateTime closeTime = new DateTime(timeData.closeTime);
        DateTime startTime = new DateTime(timeData.startTime);
        DateTime difference = new DateTime(timeDiff);

        text.text = "Close time: " + closeTime.ToString() + "\nStart time: " + startTime.ToString() + "\nDifference: " + difference.ToString();

        ApplyTime();
    }

    void SaveTimeData()
    {
        string json = JsonUtility.ToJson(timeData);
        File.WriteAllText(Application.dataPath + "/SaveData/TimeData/time.json", json);
    }

    void LoadTimeData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SaveData/TimeData/time.json");
        timeData.closeTime = JsonUtility.FromJson<TimeData>(json).closeTime;
    }

    private void OnApplicationQuit()
    {
        timeData.closeTime = DateTime.Now.Ticks;
        SaveTimeData();
    }

    void ApplyTime()
    {
        if (timeDiff > diffInterval.Ticks)
        {
            blockRef.GetComponent<Block>().DecreaseValues();
        }
    }
}
