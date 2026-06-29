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

    [SerializeField]
    private DocumentHolder _documentHolder;

    private TextHandler _textHandler;
    private AudioSource _audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textHandler = GetComponent<TextHandler>();
        _audioSource = GetComponent<AudioSource>();
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
            SetSegmentParameters(segment);

            //The wait time is calculated based on the length of the text, so that the player has enough time to read it, but it can be skipped by pressing a button
            //If the text is empty, the text part of the wait time is ignored, so that the player doesn't have to wait for nothing
            //The wait time in the segment is ignored if the text time is longer
            float textTime = _textHandler.GetTextLength() == 0 ? 0 : _baseTime + _timePerCharacter * _textHandler.GetTextLength();
            float timeToWait = textTime;
            if (segment.TimeToWait != 0)
            {
                timeToWait = segment.TimeToWait;
            }
            float targetTime = Time.time + timeToWait;
            Debug.Log("Time to wait: " + timeToWait + "Text Length: " + _textHandler.GetTextLength());

            while (keepLooping)
            {
                yield return null;
                if(Time.time >= targetTime || Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
                {
                    keepLooping = false;
                }
            }
            if(segment.DocId != null && segment.DocId != "")
            {
                _documentHolder.CollectDocument(segment.DocId, true);
            }
        }
        _textHandler.SetEmptyText();
        StopAudio();
    }

    private void SetSegmentParameters(ChapterSegment segment)
    {
        StopAudio();
        _textHandler.SetText(segment.LocId);
        if(segment.AudioOneShot)
            PlayAudioOneShot(segment.AudioClip);
        else
            PlayAudio(segment.AudioClip);
    }

    private void PlayAudio(AudioClip clip)
    {
        if(clip == null)
        {
            return;
        }
        _audioSource.clip = clip;
        _audioSource.Play();
        
    }

    private void PlayAudioOneShot(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        _audioSource.clip = clip;
        _audioSource.PlayOneShot(clip);
    }

    private void StopAudio()
    {
        _audioSource.Stop();
    }
}
