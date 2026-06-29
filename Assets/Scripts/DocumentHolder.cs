using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DocumentHolder : MonoBehaviour
{
    public struct DocumentContainer
    {
        public Document Doc;
        public bool IsNew;
        public bool IsCollected;
    }

    [SerializeField]
    private List<Document> _documents = new(); //Document Assets loaded from Addressables. It should not be modified.

    private List<DocumentContainer> _documentsList = new(); //List of DocumentContainer, which contains the Document and its state (IsNew and IsCollected). It should be modified when collecting new documents or loading from save system.

    private AsyncOperationHandle<IList<Document>> handle;

    private SaveManager _saveManager;

    [SerializeField]
    private GameObject _newIcon;

    [SerializeField]
    private AudioSource _audioSource;

    private async void Awake()
    {
        handle = Addressables.LoadAssetsAsync<Document>("Documents",null);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _documents.AddRange(handle.Result);

            foreach (Document doc in _documents)
            {
                _documentsList.Add(new DocumentContainer
                {
                    Doc = doc,
                    IsNew = false,
                    IsCollected = false
                });
            }

            _saveManager = GetComponent<SaveManager>();

            //TODO probably temporary
            LoadDocumentsFromSaveSystem();

            Debug.Log($"Loaded {_documents.Count} items");
        }
        else
        {
            Debug.LogError("Failed to load items");
        }
    }
    private void OnDestroy()
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }


    private void LoadDocumentsFromSaveSystem()
    {
        SaveFile.SavedDocument[] savedDocuments = _saveManager.GetSavedDocuments();
        foreach (SaveFile.SavedDocument doc in savedDocuments)
        {
            int index = _documentsList.FindIndex(d => d.Doc.DocID == doc.DocID);
            DocumentContainer container = _documentsList[index];
            if (container.Doc != null)
            {
                container.IsCollected = true;
                container.IsNew = doc.IsNew;
                _documentsList[index] = container;
            }
        }
    }

    public void CollectDocument(string docID)
    {
        CollectDocument(docID, false);
    }

    public void CollectDocument(string docID, bool playPrintingSound)
    {
        int index = _documentsList.FindIndex(d => d.Doc.DocID == docID);
        DocumentContainer container = _documentsList[index];
        if (container.Doc != null)
        {
            container.IsCollected = true;
            container.IsNew = true;
            _documentsList[index] = container;
        }
        else
        {
            Debug.LogError($"Document with ID {docID} not found");
        }
        if (playPrintingSound)
        {
            _audioSource.Play();
        }
        CheckNewDocuments();
    }

    public void MarkDocumentAsRead(string docID)
    {
        int index = _documentsList.FindIndex(d => d.Doc.DocID == docID);
        DocumentContainer container = _documentsList[index];
        if (container.Doc != null)
        {
            container.IsNew = false;
            _documentsList[index] = container;
        }
        else
        {
            Debug.LogError($"Document with ID {docID} not found");
        }
        CheckNewDocuments();
    }

    public DocumentContainer[] GetCollectedDocuments()
    {
        return _documentsList.FindAll(d => d.IsCollected).ToArray();
    }

    public DocumentContainer GetDocument(string docID)
    {
        int index = _documentsList.FindIndex(d => d.Doc.DocID == docID);
        if (index != -1)
        {
            return _documentsList[index];
        }
        else
        {
            Debug.LogError($"Document with ID {docID} not found");
            return default;
        }
    }

    private void CheckNewDocuments()
    { 
        if(_documentsList.FindAll(d => d.IsNew).Count > 0)
        {
            _newIcon.SetActive(true);
        }
        else
        {
            _newIcon.SetActive(false);
        }
    }

}
