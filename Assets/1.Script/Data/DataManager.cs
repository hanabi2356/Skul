using System.Collections.Generic;
using UnityEngine;
using Util;
public class DataManager : Singleton<DataManager>
{
    private Dictionary<string, SkulStatData> skulStatTable = new Dictionary<string, SkulStatData>();

    private const string SkulStatDataPath = "Data/Skul";

    protected override void Awake()
    {
        base.Awake();
        InitData();
    }
    private void InitData()
    {
        LoadTable(skulStatTable, SkulStatDataPath);
    }
    private void LoadTable<T>(Dictionary<string, T> table, string path) where T : ScriptableObject
    {
        T[] asset = Resources.LoadAll<T>(path);
        foreach(var assetItem in asset)
        {
            table.Add(assetItem.name, assetItem);
        }
    }
    public SkulStatData GetSkulStatData(string name) => skulStatTable.GetValueOrDefault(name);

}
