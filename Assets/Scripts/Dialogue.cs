using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dialogue;
    [SerializeField]
    float speed;
    [SerializeField]
    Hamster hamsterRef;

    string[] lines;
    int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        index = 0;
        dialogue.text = "";
        lines = hamsterRef.GetLines();
        gameObject.SetActive(true);
        StartCoroutine(NextLine());
    }

    public IEnumerator NextLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogue.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    public void NextDialogue()
    {
        if (dialogue.text == lines[index])
        {
            index++;

            if (index >= lines.Length)
            {
                gameObject.SetActive(false);
                return;
            }

            dialogue.text = "";
            StartCoroutine (NextLine());
        }
        else
        {
            dialogue.text = lines[index];
            StopAllCoroutines();
        }
    }
}
