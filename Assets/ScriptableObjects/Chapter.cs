using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "ScriptableObjects/Chapter", order = 3)]
public class Chapter : ScriptableObject
{
    public string Name;
    public ChapterSegment[] Segments;
}

