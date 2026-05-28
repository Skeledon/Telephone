using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneBookEntry", menuName = "ScriptableObjects/PhoneBookEntry", order = 2)]
public class PhoneBookEntry : ScriptableObject
{
    public string Name;
    public string PhoneNumber;
    public Chapter Chapter;
}

