using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterSegment", menuName = "ScriptableObjects/ChapterSegment", order = 3)]
public class ChapterSegment : ScriptableObject
{
    public string LocId;

    [Tooltip("Time to wait before starting the next chapter segment. If this is 0 it uses the text length")]
    public float TimeToWait;

    public AudioClip AudioClip;
    public bool AudioOneShot = false;
    //TODO add doc to print

    public string DocId;
}

