using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DocumentsUIManager : MonoBehaviour
{
    [SerializeField]
    private DocumentHolder _holder;

    private int _currentIndex = 0;

    private string[] _documentsToShow;


    //Temporary interface for testing, to be replaced with the actual UI
    public LocalizeSpriteEvent LeftObject;
    public LocalizeSpriteEvent RightObject;
    public LocalizeSpriteEvent MiddleObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    private void OnEnable()
    {
        DocumentHolder.DocumentContainer[] container = _holder.GetCollectedDocuments();
        _documentsToShow = new string[container.Length];
        for (int i = 0; i < container.Length; i++)
        {
            _documentsToShow[i] = container[i].Doc.DocID;
        }

        SetImages();

    }

    private void SetImages()
    {
        MiddleObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex];
        if (_currentIndex + 1 < _documentsToShow.Length)
        {
            RightObject.GetComponent<Image>().enabled = true;
            RightObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex + 1];
        }
        else
        {
            RightObject.AssetReference.TableEntryReference = string.Empty;
            RightObject.GetComponent<Image>().enabled = false;
        }

        if (_currentIndex >= 1)
        {
            LeftObject.GetComponent<Image>().enabled = true;
            LeftObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex - 1];
        }
        else
        {
            LeftObject.AssetReference.TableEntryReference = string.Empty;
            LeftObject.GetComponent<Image>().enabled = false;
        }


    }

    private void ManageInput()
    {
        //TODO Temporary
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (_currentIndex >= 1)
            {
                _currentIndex--;
                SetImages();
            }
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (_currentIndex + 1 < _documentsToShow.Length)
            {
                _currentIndex++;
                SetImages();
            }
        }
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
