using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class Door : MonoBehaviour
{
    [SerializeField]
    HamsterStats stats;

    TextMeshProUGUI textRef;
    Image imageRef;
    Color colour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textRef = GetComponentInChildren<TextMeshProUGUI>();
        imageRef = GetComponent<Image>();
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        colour = Color.white;
        textRef.text = "";

        if (stats.thirst < stats.maxThirst / 2.0f)
        {
            textRef.text += "Thirsty";
            colour.g -= 0.2f;
            colour.b -= 0.2f;
            imageRef.color = colour;
        }
        if (stats.hunger < stats.maxHunger / 2.0f)
        {
            textRef.text += "Hungry";
            colour.g -= 0.2f;
            colour.b -= 0.2f;
            imageRef.color = colour;
        }
        if (stats.attention < stats.maxAttention / 2.0f)
        {
            textRef.text += "Bored";
            colour.g -= 0.2f;
            colour.b -= 0.2f;
            imageRef.color = colour;
        }
    }

    public void EnterCage()
    {
        GameData.hamster = stats;
        SceneManager.LoadScene("Cage");
    }

    public void DecreaseValues()
    {
        stats.hunger = 0;
        stats.thirst = 0;
        stats.attention = 0;
    }

    public void KillCheck()
    {
        if (stats.happiness == 0)
        {
            GameData.death = true;
            GameData.deathName = stats.title;
            stats.alive = false;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SaveData/HamsterData/" + stats.title + ".json");
        HamsterData hamsterData;
        hamsterData = JsonUtility.FromJson<HamsterData>(json);

        stats.hunger = hamsterData.hunger;
        stats.thirst = hamsterData.thirst;
        stats.attention = hamsterData.attention;
        stats.happiness = hamsterData.happiness;
    }
}
