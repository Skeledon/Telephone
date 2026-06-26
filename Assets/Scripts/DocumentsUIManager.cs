using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DocumentsUIManager : MonoBehaviour
{
    [SerializeField]
    private DocumentHolder _holder;

    [SerializeField]
    private AudioClip _turnPageClip;

    [SerializeField]
    private AudioSource _audioSource;

    private int _currentIndex = 0;

    private string[] _documentsToShow;


    //Temporary interface for testing, to be replaced with the actual UI
    //public LocalizeSpriteEvent LeftObject;
    //public LocalizeSpriteEvent RightObject;
    //public LocalizeSpriteEvent MiddleObject;

    [SerializeField]
    private DocumentUIItem _leftObject;

    [SerializeField]
    private DocumentUIItem _rightObject;

    [SerializeField]
    private DocumentUIItem _middleObject;
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
        //MiddleObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex];
        _middleObject.SetImage(_holder.GetDocument(_documentsToShow[_currentIndex]));
        _holder.MarkDocumentAsRead(_documentsToShow[_currentIndex]);

        if (_currentIndex + 1 < _documentsToShow.Length)
        {
            //RightObject.GetComponent<Image>().enabled = true;
            //RightObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex + 1];
            _rightObject.SetImage(_holder.GetDocument(_documentsToShow[_currentIndex + 1]));
            _rightObject.SetVisible(true);
        }
        else
        {
            //RightObject.AssetReference.TableEntryReference = string.Empty;
            //RightObject.GetComponent<Image>().enabled = false;
            _rightObject.SetVisible(false);
        }

        if (_currentIndex >= 1)
        {
            //LeftObject.GetComponent<Image>().enabled = true;
            //LeftObject.AssetReference.TableEntryReference = _documentsToShow[_currentIndex - 1];
            _leftObject.SetImage(_holder.GetDocument(_documentsToShow[_currentIndex - 1]));
            _leftObject.SetVisible(true);
        }
        else
        {
            //LeftObject.AssetReference.TableEntryReference = string.Empty;
            //LeftObject.GetComponent<Image>().enabled = false;
            _leftObject.SetVisible(false);
        }


    }

    private void ManageInput()
    {
        //TODO Temporary
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            Previous();
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            Next();
        }
    }

    public void Previous()
    {
        if (_currentIndex >= 1)
        {
            _currentIndex--;
            SetImages();
            _audioSource.PlayOneShot(_turnPageClip);
        }
    }

    public void Next()
    {
        if (_currentIndex + 1 < _documentsToShow.Length)
        {
            _currentIndex++;
            SetImages();
            _audioSource.PlayOneShot(_turnPageClip);
        }
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
