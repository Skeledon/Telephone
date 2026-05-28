using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private float _baseTime = 3f;

    [SerializeField]
    private float _timePerCharacter = 0.1f;

    private TextHandler _textHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textHandler = GetComponent<TextHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteChapter(Chapter chapter)
    {
        StartCoroutine(ExecuteSegments(chapter));
        /*//TODO da completare con tutte le funzionaltà
        ChapterSegment[] segments = chapter.Segments;
        foreach (ChapterSegment segment in segments)
        {

            _textHandler.SetText(segment.LocId);
            
        }*/
    }

    private IEnumerator ExecuteSegments(Chapter chapter)
    {
        foreach (ChapterSegment segment in chapter.Segments)
        {
            bool keepLooping = true;
            _textHandler.SetText(segment.LocId);

            float targetTime = Time.time + _baseTime + _timePerCharacter*_textHandler.GetTextLength();
            Debug.Log(("Time.time = " + Time.time + " targetTime = " + targetTime));
            while (keepLooping)
            {
                yield return null;
                if(Time.time >= targetTime || Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
                {
                    keepLooping = false;
                }
            }
        }
        _textHandler.SetEmptyText();
    }
}
