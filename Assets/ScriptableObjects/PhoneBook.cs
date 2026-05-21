using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneBook", menuName = "ScriptableObjects/PhoneBook", order = 1)]
public class PhoneBook : ScriptableObject
{
    public PhoneBookEntry[] Entries;
}
