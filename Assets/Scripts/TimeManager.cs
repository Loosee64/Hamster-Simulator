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
    bool running = false;


    [SerializeField]
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diffInterval = new DateTime();
        diffInterval = diffInterval.AddSeconds(2);

        timeData = new TimeData();

        if (running)
        {
            LoadTimeData();
        }

        timeData.startTime = DateTime.Now.Ticks;

        if (timeData.closeTime != 0)
        {
            timeDiff = timeData.startTime - timeData.closeTime;
        }

        DateTime closeTime = new DateTime(timeData.closeTime);
        DateTime startTime = new DateTime(timeData.startTime);
        DateTime difference = new DateTime(timeDiff);

        if (running)
        {
            text.text = "Close time: " + closeTime.ToString() + "\nStart time: " + startTime.ToString() + "\nDifference: " + difference.ToString();
        }

        if (running)
        {
            ApplyTime();
        }
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
        if (running)
        {
            timeData.closeTime = DateTime.Now.Ticks;
            SaveTimeData();
        }
    }

    void ApplyTime()
    {
        if (timeDiff > diffInterval.Ticks)
        {
            blockRef.GetComponent<Block>().DecreaseValues();
        }
    }

    public void InvertTime()
    {
        running = !running;
        text.text = "";
    }
}
