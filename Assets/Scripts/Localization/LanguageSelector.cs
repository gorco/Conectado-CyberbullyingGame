using System.Collections.Generic;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public static LanguageSelector instance;

    private static List<TextAsset> jsonFiles;
    private static Dictionary<string, string> myDictionary;
    private static string Language;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //Selects a language by flag button in Title scene
    public void SetLanguage(string lang)
    {
        Language = lang;
        SetUpJSONFiles();
        FillDictionary();
    }

    public string GetCurrentLanguage() { return Language; }

    //Creates an initializes Json Files array to be used by myDictionary
    void SetUpJSONFiles()
    {
        Object[] filler = Resources.LoadAll("Localization/" + Language + "/" + "Dictionaries", typeof(TextAsset));

        if (filler == null || filler.Length == 0)
        {
            Debug.LogError("No JSON Files in Dictionaries directory found!");
        }

        jsonFiles = new List<TextAsset>();
        foreach (Object file in filler)
        {
            jsonFiles.Add((TextAsset)file);
        }

#if UNITY_EDITOR
        foreach (var t in jsonFiles)
            Debug.Log("JSON File added: " + t.name);
#endif

    }

    //Fills myDictionary with all JSON files
    void FillDictionary()
    {
        myDictionary = new Dictionary<string, string>();

        string fileContents;
        Dictionary<string, string> auxDic;

        foreach (var jsonFile in jsonFiles)
        {
            fileContents = jsonFile.text;

            JSONObject jsonObj = JSONObject.Create(fileContents);

            auxDic = jsonObj.ToDictionary();

            foreach (var entry in auxDic)
                myDictionary.Add(entry.Key, entry.Value);
        }

    }

    //Checks if the given key "objectName" is in myDictionary, if it's not, logs error; 
    //otherwise returns the string of the given key.
    public string GetName(string objectName)
    {
        if (!myDictionary.ContainsKey(objectName))
        {
            Debug.LogError("The sequence with key " + objectName + " doesn't exit (Object " + this.gameObject.name + ")");
            return null;
        }

        string newWord = myDictionary[objectName];
        if (newWord.Contains("\\n"))
            newWord = myDictionary[objectName].Replace("\\n", "\n");

        return newWord;
    }
}
