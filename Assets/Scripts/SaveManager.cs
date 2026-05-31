using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SaveFile _saveFile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void LoadSaveFile(SaveFile saveFile)
    {
        _saveFile = saveFile;
    }

    public SaveFile.SavedDocument[] GetSavedDocuments()
    {
        if (_saveFile == null)
        {
            Debug.LogError("Save file is null");
            return new SaveFile.SavedDocument[0];
        }
        return _saveFile.GetSavedDocuments();
    }

}
