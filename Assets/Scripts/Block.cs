using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    [SerializeField]
    RectTransform doorMainRef;
    Door[] doors;

    public void Awake()
    {
        if (doors == null)
        {
            doors = doorMainRef.GetComponentsInChildren<Door>();
        }
    }

    public void Update()
    {
        foreach (Door door in doors)
        {
            door.GetComponent<Door>().KillCheck();
        }
    }

    public void DecreaseValues()
    {
        foreach (Door door in doors)
        {
            door.GetComponent<Door>().DecreaseValues();
        }
    }
}
