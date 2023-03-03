using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using QuickStart.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button loadButton;
    
    void Start () {
        SaveUtils.Init();
        
        Button btn = saveButton.GetComponent<Button>();
        btn.onClick.AddListener(Save);
        
        Button btn2 = loadButton.GetComponent<Button>();
        btn2.onClick.AddListener(Load);
    }

    private void Save()
    {
        string value = "";
        PlacedObject.instances.ForEach(p => value += JsonUtility.ToJson(new SaveObject(p.objectTypeId, p.origin, p.dir.ToString(), p.positionType.ToString())) + "\n");
        SaveUtils.Save(value,inputField.text);
        Debug.Log(value);
    }

    private void Load()
    {
        string[] result = SaveUtils.Load(inputField.text).Split("\n");
        List<SaveObject> toAdd = new List<SaveObject>();
        
        foreach (string s in result)
        {
            toAdd.Add(JsonUtility.FromJson<SaveObject>(s));
        }
        
        GridSystem.Instance.LoadAllObjects(toAdd);
    }
}
