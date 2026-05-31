using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "ScriptableObjects/SaveFile", order = 6)]
public class SaveFile : ScriptableObject
{
    [Serializable]
    public struct SavedDocument
    {
        public string DocID;
        public bool IsNew;
    }

    [SerializeField]
    private SavedDocument[] SavedDocuments;

    public SavedDocument[] GetSavedDocuments()
    {
        return SavedDocuments;
    }


}
