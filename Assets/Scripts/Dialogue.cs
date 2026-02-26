using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    string[] lines;
    [SerializeField]
    TextMeshProUGUI dialogue;
    [SerializeField]
    float speed;

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
}
