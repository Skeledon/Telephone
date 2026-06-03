using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    private string _currentNumber;

    private PhoneBookHandler _phoneBookHandler;
    private TextHandler _textHandler;
    private StoryManager _storyManager;

    public GameObject HangUpButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentNumber = string.Empty;
        _phoneBookHandler = GetComponent<PhoneBookHandler>();
        _textHandler = GetComponent<TextHandler>();
        _storyManager = GetComponent<StoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhoneButtonPressed(string key)
    {
        HangUpButton.SetActive(true);
        AddToNumber(key);
    }

    private void AddToNumber(string number)
    {
        _currentNumber += number;
        Chapter c = _phoneBookHandler.GetChapterForPhoneNumber(_currentNumber);
        if (c != null)
        {
            _storyManager.ExecuteChapter(c);
            ResetPhone();
        }

    }

    public void ResetPhone()
    {
        _currentNumber = string.Empty;
        HangUpButton.SetActive(false);
    }
}
