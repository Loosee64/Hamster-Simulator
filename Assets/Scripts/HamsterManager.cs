using UnityEngine;

public class HamsterManager : MonoBehaviour
{
    [SerializeField]
    Hamster hamsterRef;

    HamsterStats[] stats;
    int index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = Resources.LoadAll<HamsterStats>("Hamsters/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHamster()
    {
        index++;

        if (index >= stats.Length)
        {
            index = 0;
        }

        hamsterRef.SetHamster(stats[index]);
    }
}
