#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnemyStatExporter : EditorWindow
{

    [MenuItem("Tools/Enemy Stat Exporter")]
    public static void ShowWindow()
    {
        GetWindow<EnemyStatExporter>("Enemy Stat Exporter");
    }
    void OnGUI()
    {
        if(GUILayout.Button("Export All Enemy Stat"))
        {
          
            ExportAllEnemyStat();
        }
       

    }

    private void ExportAllEnemyStat()
    {
        var assets = AssetDatabase.FindAssets("t:EnemyStatData");
        var table = new EnemyStatTableDTO();

        foreach(var guid in assets)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var so = AssetDatabase.LoadAssetAtPath<EnemyStatData>(path);
            if(so == null) continue;

            table.EnemyStatDataList.Add(ToDTO(so));
        }

        var folderPath = Application.streamingAssetsPath;
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var filePath = Path.Combine(folderPath, "EnemyStatTable.json");
        File.WriteAllText(filePath, JsonUtility.ToJson(table,true));
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Enemy Stat Exported", "OK");
    }
    
   
	static EnemyStatDataDTO ToDTO(EnemyStatData data) => new EnemyStatDataDTO
	{
		EnemyID = data.EnemyID,
		EnemyName = data.Name,
		EnemyRarity = data.EnemyRarity.ToString(),
		MaxHP = data.MaxHP,
		AttackType = data.AttackType.ToString(),
		AttackPower = data.AttackPower,
		AttackSpeed = data.AttackSpeed,
		AttackCoolTime = data.AttackCoolTime,
		AttackRange = data.AttackRange,
		DetectedRange = data.DetectedRange,
		MoveSpeed = data.MoveSpeed
	};
}
#endif
