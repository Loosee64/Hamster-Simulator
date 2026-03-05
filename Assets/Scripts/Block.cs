using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    [SerializeField]
    RectTransform doorMainRef;
    Door[] doors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void DecreaseValues()
    {
        doors = doorMainRef.GetComponentsInChildren<Door>();

        foreach (Door door in doors)
        {
            door.GetComponent<Door>().DecreaseValues();
        }
    }
}
