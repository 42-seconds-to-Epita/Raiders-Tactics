using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveUtils : MonoBehaviour
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "json";

    public static void Init() {
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    
    public static void Save(string saveString, string fileName) {
        File.WriteAllText(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION, saveString);
    }

    public static string Load(string fileName)
    {
        return File.ReadAllText(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION);
    }
}
