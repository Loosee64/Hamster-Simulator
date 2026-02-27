using System.Collections;
using TMPro;
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
    [SerializeField]
    DialogueLines linesRef;

    bool thirsty = false;
    bool thirstDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        title.text = "Name: " + stats.title;
        hunger.text = "Hunger: " + stats.hunger.ToString();
        thirst.text = "Thirst: " + stats.thirst.ToString();
        attention.text = "Attention: " + stats.attention.ToString();
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
            waterRef.decreaseValue();
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

        drink();
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

    public string[] GetLines()
    {
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
            }
        }

        return lines;
    }
}
