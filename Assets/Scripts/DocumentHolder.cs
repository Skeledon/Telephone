using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DocumentHolder : MonoBehaviour
{
    [SerializeField]
    private List<Document> _documents = new();

    private AsyncOperationHandle<IList<Document>> handle;

    private async void Awake()
    {
        handle =Addressables.LoadAssetsAsync<Document>("Documents",null);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _documents.AddRange(handle.Result);

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
