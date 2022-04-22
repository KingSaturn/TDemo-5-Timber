using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private readonly List<int> modifiers = new List<int>();
    
    public int GetValue()
    {
        return baseValue;
    }

    public int CalculateValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }
    
    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }

    public void CleanseModifiers()
    {
        modifiers.Clear();
    }
}
