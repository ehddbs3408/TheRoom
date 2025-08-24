using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum KeyType
{
    None,
    UpArrow,
    DownArrow,
    LeftArrow,
    RightArrow,
    Interaction,

}
[Serializable]
public class KeyData
{
    public KeyType keyType;
    public KeyCode keyCode;

    public KeyData(KeyType keyType,KeyCode keyCode)
    {
        this.keyType = keyType;
        this.keyCode = keyCode;
    }
}

public class KeyDataList
{
    public List<KeyData> datas;
}

public class KeyManager : Singleton<KeyManager>
{
    KeyDataList keydataList;

    Dictionary<KeyType, KeyCode> keyTypeDictionary = new Dictionary<KeyType, KeyCode>();

    private void Awake()
    {
        LoadKeyData();
    }

    private void LoadKeyData()
    {
        keydataList = JsonLoader.LoadJsonFile<KeyDataList>($"{Application.dataPath}/Save", "KeySetting");

        if(keydataList.datas.Count <= 1 )
        {
            KeyDataInit();
            Debug.Log("KeyDataList Init");
        }

        foreach(var keyData in keydataList.datas)
        {
            keyTypeDictionary.Add(keyData.keyType,keyData.keyCode);
        }
    }

    private void KeyDataInit()
    {
        keydataList.datas.Add(new KeyData(KeyType.UpArrow,KeyCode.UpArrow));
        keydataList.datas.Add(new KeyData(KeyType.DownArrow, KeyCode.DownArrow));
        keydataList.datas.Add(new KeyData(KeyType.LeftArrow, KeyCode.LeftArrow));
        keydataList.datas.Add(new KeyData(KeyType.RightArrow, KeyCode.RightArrow));
        keydataList.datas.Add(new KeyData(KeyType.Interaction, KeyCode.F));

        SaveKeyData();
    }

    private void SaveKeyData()
    {
        JsonLoader.SaveJsonFile($"{Application.dataPath}/Save", "KeySetting", JsonLoader.ObjectToJson(keydataList));
    }

    public void AssignKey(KeyType keyType,KeyCode changeKeyCode)
    {
        if(keyTypeDictionary.ContainsKey(keyType))
        {
            Debug.Log($"already set up KeyCode : {keyType}");
        }

        if (keyTypeDictionary[keyType] == changeKeyCode)
        {
            Debug.Log($"it's same keyCode : {keyType}");
        }

        keyTypeDictionary[keyType] = changeKeyCode;
    }

    public bool CheckKey(KeyCode keyCode,KeyType keyType)
    {
        if (keyTypeDictionary[keyType] == keyCode) return true;

        return false;
    }

    public KeyCode GetkeyCode(KeyType keyType)
    {
        return keyTypeDictionary[keyType];
    }
}
