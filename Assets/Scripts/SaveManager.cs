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

    // ===== ENCRYPTION KEYS (change these!) =====
    // IMPORTANT: replace with your own random strings
    private static readonly string aesKey = "TetteCuloChiappeENonArrivoA32Uff";
    private static readonly string hmacKey = "ChiaveSuperSegretaPewPewPewPeeeW";

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

    [Serializable]
    private class SecureWrapper
    {
        public string data;   // encrypted payload
        public string hash;   // HMAC for tamper detection
    }

    public void SaveGame(SaveData save)
    {
        string json = JsonUtility.ToJson(save);

        string encrypted = Encrypt(json);
        string hash = ComputeHMAC(encrypted);

        SecureWrapper wrapper = new SecureWrapper
        {
            data = encrypted,
            hash = hash
        };

        string finalJson = JsonUtility.ToJson(wrapper);
        File.WriteAllText(savePath, finalJson);

        Debug.Log("Game saved.");
    }

    public SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        string json = File.ReadAllText(savePath);
        SecureWrapper wrapper = JsonUtility.FromJson<SecureWrapper>(json);

        // Verify integrity
        string expectedHash = ComputeHMAC(wrapper.data);
        if (expectedHash != wrapper.hash)
        {
            Debug.LogWarning("Save file tampered or corrupted!");
            return null;
        }

        string decrypted = Decrypt(wrapper.data);
        return JsonUtility.FromJson<SaveData>(decrypted);
    }

    // =========================================================
    // ENCRYPTION (AES)
    // =========================================================

    private string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(aesKey);
            aes.GenerateIV();

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private string Decrypt(string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(aesKey);

            byte[] iv = new byte[16];
            Array.Copy(fullCipher, iv, iv.Length);
            aes.IV = iv;

            byte[] cipher = new byte[fullCipher.Length - iv.Length];
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipher))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

    // =========================================================
    // INTEGRITY CHECK (HMAC)
    // =========================================================

    private string ComputeHMAC(string data)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmacKey)))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }
}
