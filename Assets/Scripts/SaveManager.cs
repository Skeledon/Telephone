using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // ===== FILE PATHS =====
    private string settingsPath;
    private string savePath;



    void Awake()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
        savePath = Path.Combine(Application.persistentDataPath, "save.dat");

        Debug.Log("Save folder: " + Application.persistentDataPath);
    }

    // =========================================================
    // SETTINGS (PLAIN TEXT - PLAYER EDITABLE)
    // =========================================================

    [Serializable]
    public class SettingsData
    {
        public int MusicVolume = 80;
        public int SfxVolume = 80;
        public bool FullScreen = true;
        public int ResolutionX = 1920;
        public int ResolutionY = 1080;
        public string Language = "en";
    }

    public void SaveSettings(SettingsData settings)
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(settingsPath, json);
    }

    public SettingsData LoadSettings()
    {
        if (!File.Exists(settingsPath))
            return new SettingsData();

        string json = File.ReadAllText(settingsPath);
        return JsonUtility.FromJson<SettingsData>(json);
    }

    // =========================================================
    // GAME SAVE (ENCRYPTED + INTEGRITY PROTECTED)
    // =========================================================

    [Serializable]
    public class SaveData
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



    public void SaveGame(SaveData save)
    {
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(savePath, json);

        Debug.Log("Game saved.");
    }

    public SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        //TODO aggiungere loadgame
        return null;
    }


    // =========================================================
    // UTILITY METHODS
    // =========================================================
}
