using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class TextHandler : MonoBehaviour
{
    public GameObject TextItem;

    private TextMeshProUGUI _tmProText;
    private LocalizeStringEvent _localizeStringEvent;

    private bool _isTextEmpty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tmProText = TextItem.GetComponent<TextMeshProUGUI>();
        _localizeStringEvent = TextItem.GetComponent<LocalizeStringEvent>();
    }

    public void SetText(string locId)
    {
        if(locId == null || locId == string.Empty)
        {
            SetEmptyText();
            return;
        }
        _isTextEmpty = false;
        _localizeStringEvent.StringReference.TableEntryReference = locId;
        _localizeStringEvent.RefreshString();
    }

    public void SetEmptyText()
    {
        _localizeStringEvent.StringReference.TableEntryReference = string.Empty;
        _localizeStringEvent.RefreshString();
        _tmProText.text = string.Empty;
        _isTextEmpty = true;
    }

    public int GetTextLength()
    {
        if(_isTextEmpty)
        {
            return 0;
        }
        return _localizeStringEvent.StringReference.GetLocalizedString().Length;

    }
}
