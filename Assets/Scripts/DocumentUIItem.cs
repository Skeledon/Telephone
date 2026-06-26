using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DocumentUIItem : MonoBehaviour
{
    [SerializeField]
    private Image _mainImage;

    [SerializeField]
    private Image _newIcon;

    [SerializeField]
    private LocalizeSpriteEvent _localizeSpriteEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(DocumentHolder.DocumentContainer document)
    {
        _localizeSpriteEvent.AssetReference.TableEntryReference = document.Doc.DocID;
        if(document.IsNew)
        {
            _newIcon.gameObject.SetActive(true);
        }
        else
        {
            _newIcon.gameObject.SetActive(false);
        }
    }

    public void SetVisible(bool visible)
    {
        _mainImage.gameObject.SetActive(visible);

    }
}
