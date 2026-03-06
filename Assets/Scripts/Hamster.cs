using System.Collections;
using TMPro;
using System.IO;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    [SerializeField]
    HamsterStats stats;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI hunger;
    [SerializeField]
    TextMeshProUGUI thirst;
    [SerializeField]
    TextMeshProUGUI attention;
    [SerializeField]
    WaterBar waterRef;
    
    DialogueLines linesRef;

    bool thirsty = false;
    bool thirstDown = false;

    bool hungerDown = false;

    bool greeting = true;
    int lastDialogue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = GameData.hamster;
        ResetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (!thirstDown)
        {
            StartCoroutine(thirstyTimer());
        }

        if (thirsty)
        {
            StartCoroutine(drinkTimer());
        }

        if (!hungerDown)
        {
            StartCoroutine(hungryTimer());
        }
    }

    void ResetStats()
    {
        title.text = "Name: " + stats.title;
        hunger.text = "Hunger: " + stats.hunger.ToString();
        thirst.text = "Thirst: " + stats.thirst.ToString();
        attention.text = "Attention: " + stats.attention.ToString();
    }

    public void feed()
    {
        if (stats.maxHunger > stats.hunger)
        {
            stats.hunger++;
        }
        else
        {
            stats.hunger = stats.maxHunger;
        }
        hunger.text = "Hunger: " + stats.hunger.ToString();
    }

    public void talk()
    {
        if (stats.maxAttention > stats.attention)
        {
            stats.attention++;
        }
        else
        {
            stats.attention = stats.maxAttention;
        }
        attention.text = "Attention: " + stats.attention.ToString();
    }

    public void drink()
    {
        if (stats.thirst < stats.maxThirst)
        {
            waterRef.DecreaseValue();
            stats.thirst++;
        }
        else
        {
            stats.thirst = stats.maxThirst;
        }

        if (stats.thirst < stats.maxThirst)
        {
            thirsty = true;
        }

        thirst.text = "Thirst: " + stats.thirst.ToString();
    }

    IEnumerator drinkTimer()
    {
        thirsty = false;

        yield return new WaitForSeconds(1.0f);

        if (waterRef.CanDrink())
        {
            drink();
        }
    }

    IEnumerator thirstyTimer()
    {
        thirstDown = true;

        yield return new WaitForSeconds(6.0f);

        if (stats.thirst > 0)
        {
            stats.thirst--;
            thirst.text = "Thirst: " + stats.thirst.ToString();
        }

        if (stats.thirst < stats.maxThirst && !thirsty)
        {
            thirsty = true;
        }

        thirstDown = false;
    }

    IEnumerator hungryTimer()
    {
        hungerDown = true;
        yield return new WaitForSeconds(10.0f);
        if (stats.hunger > 0)
        {
            stats.hunger--;
        }
        hungerDown = false;
        ResetStats();
    }

    public string[] GetLines()
    {
        if (GameData.death)
        {
            LoadDeathLine();
            greeting = false;
            GameData.death = false;
        }
        else
        {
            if (greeting)
            {
                LoadGreeting();
                greeting = false;
            }
            else
            {
                LoadLine("Basic");
            }
        }
        

        string[] lines = new string[linesRef.lines.Length];
        linesRef.lines.CopyTo(lines, 0);

        for (int index = 0; index < lines.Length; index++)
        {
            for(int index2 = 0; index2 < lines[index].Length; index2++)
            {
                if (lines[index][index2] == '-')
                {
                    lines[index] = lines[index].Remove(index2, 1);
                    lines[index] = lines[index].Insert(index2, stats.title);
                    lines[index].ToUpper();
                }
                if (lines[index][index2] == '=')
                {
                    lines[index] = lines[index].Remove(index2, 1);
                    lines[index] = lines[index].Insert(index2, GameData.deathName);
                    lines[index].ToUpper();
                }
            }
        }

        return lines;
    }

    public void SetHamster(HamsterStats t_ham)
    {
        stats = t_ham;
        ResetStats();
    }

    public void SaveData()
    {
        HamsterData hamsterData = new HamsterData();
        hamsterData.hunger = stats.hunger;
        hamsterData.thirst = stats.thirst;
        hamsterData.attention = stats.attention;
        hamsterData.alive = stats.alive;
        hamsterData.happiness = stats.happiness;

        string json = JsonUtility.ToJson(hamsterData);
        File.WriteAllText(Application.dataPath + "/SaveData/HamsterData/" + stats.title +  ".json", json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SaveData/HamsterData/" + stats.title + ".json");
        HamsterData hamsterData;
        hamsterData = JsonUtility.FromJson<HamsterData>(json);

        stats.hunger = hamsterData.hunger;
        stats.thirst = hamsterData.thirst;
        stats.attention = hamsterData.attention;
        stats.alive = hamsterData.alive;
        stats.happiness = hamsterData.happiness;
    }

    public void LoadGreeting()
    {
        linesRef = Resources.Load<DialogueLines>("Lines/" + stats.personality + "/Greetings/Greeting");
    }

    public void LoadLine(string t_type)
    {
        int rand = Random.Range(1, 3);
        while(lastDialogue == rand)
        {
            rand = Random.Range(1, 3);
        }

        lastDialogue = rand;

        linesRef = Resources.Load<DialogueLines>("Lines/" + stats.personality + "/" + t_type + "/" + t_type + rand.ToString());
    }

    public void LoadDeathLine()
    {
        int rand = Random.Range(1, 3);

        linesRef = Resources.Load<DialogueLines>("Lines/Death/Death" + rand.ToString());
    }
}
