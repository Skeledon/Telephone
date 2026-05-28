using UnityEngine;

public class PhoneBookHandler : MonoBehaviour
{
    [SerializeField]
    private PhoneBook _phoneBook;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FindPhoneNumber(string number)
    {
        foreach (PhoneBookEntry entry in _phoneBook.Entries)
        {
            if (entry.PhoneNumber == number)
            {
                return true;
            }
        }
        return false;
    }

    public Chapter GetChapterForPhoneNumber(string number)
    {
        foreach (PhoneBookEntry entry in _phoneBook.Entries)
        {
            if (entry.PhoneNumber == number)
            {
                return entry.Chapter;
            }
        }
        return null;
    }
}
