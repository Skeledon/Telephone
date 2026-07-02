using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    // ===== FILE PATHS =====
    private string settingsPath;
    private string savePath;



    void Awake()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        Debug.Log("Save folder: " + Application.persistentDataPath);

    }

    // =========================================================
    // SETTINGS (PLAIN TEXT - PLAYER EDITABLE)
    // =========================================================

    [Serializable, HideInInspector]
    public class SettingsData
    {
        public float MusicVolume = 1;
        public float SfxVolume = 1;
        public float MasterVolume = 1;
        public bool FullScreen = false;
        public int ResolutionX = 1920;
        public int ResolutionY = 1080;
        public string Language = "en";
    }

    public void SaveSettings(SettingsData settings)
    {
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(settingsPath, json);

        Debug.Log("Settings saved.");
    }

    public SettingsData LoadSettings()
    {
        if (!File.Exists(settingsPath))
            return new SettingsData();

        string json = File.ReadAllText(settingsPath);
        return JsonConvert.DeserializeObject<SettingsData>(json);
    }

    [Serializable]
    public class SaveData
    {

        [Serializable]
        public struct SavedDocument
        {
            public string DocID;
            public bool IsNew;
        }

        //Required for Json Deserialization
        public SaveData()
        { }

        public SaveData(SavedDocument[] documents)
        {
            SavedDocuments = documents;
        }

        public SaveData(DocumentHolder.DocumentContainer[] documents)
        {
            SavedDocument[] tmp = new SavedDocument[documents.Length];
            for(int i = 0; i< documents.Length; i++)
            {
                tmp[i].DocID = documents[i].Doc.DocID;
                tmp[i].IsNew = documents[i].IsNew;
            }
            SavedDocuments = tmp;
        }

        public SavedDocument[] SavedDocuments;
    }



    public void SaveGame(SaveData save)
    {
        string json = JsonConvert.SerializeObject(save, Formatting.Indented);

        File.WriteAllText(savePath, json);

        Debug.Log("Game saved.");
    }

    public SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        //TODO aggiungere loadgame
        string json = File.ReadAllText(savePath);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }


    // =========================================================
    // UTILITY METHODS
    // =========================================================
}
