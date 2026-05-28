using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class TextHandler : MonoBehaviour
{
    public GameObject TextItem;

    private TextMeshProUGUI _tmProText;
    private LocalizeStringEvent _localizeStringEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tmProText = TextItem.GetComponent<TextMeshProUGUI>();
        _localizeStringEvent = TextItem.GetComponent<LocalizeStringEvent>();
    }

    public void SetText(string locId)
    {
        _localizeStringEvent.StringReference.TableEntryReference = locId;
        _localizeStringEvent.RefreshString();
    }

    public void SetEmptyText()
    {
        _localizeStringEvent.StringReference.TableEntryReference = string.Empty;
        _localizeStringEvent.RefreshString();
        _tmProText.text = string.Empty;
    }

    public int GetTextLength()
    {
        return _localizeStringEvent.StringReference.GetLocalizedString().Length;

    }
}
