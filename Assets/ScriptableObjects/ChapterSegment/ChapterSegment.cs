using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterSegment", menuName = "ScriptableObjects/ChapterSegment", order = 3)]
public class ChapterSegment : ScriptableObject
{
    public string LocId;
    public float TimeToWait;

    public AudioClip AudioClip;
    public bool AudioOneShot = false;
    //TODO add doc to print
}

