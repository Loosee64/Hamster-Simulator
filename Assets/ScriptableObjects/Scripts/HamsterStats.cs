using UnityEngine;

[CreateAssetMenu(fileName = "HamsterState", menuName = "Scriptable Objects/HamsterState")]
public class HamsterStats : ScriptableObject
{
    public string title = "";
    public int hunger = 0;
    public int thirst = 0;
    public int attention = 0;

    public int maxHunger = 10;
    public int maxThirst = 10;
    public int maxAttention = 10;
}
