using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    private string currentNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentNumber = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhoneButtonPressed(string key)
    {
        AddToNumber(key);
    }

    private void AddToNumber(string number)
    {
        currentNumber += number;
        Debug.Log(currentNumber);
    }
}
