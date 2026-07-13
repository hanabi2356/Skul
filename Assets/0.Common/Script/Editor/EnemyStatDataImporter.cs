#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
public class EnemyStatDataImporter : EditorWindow
{
	private string csvPath = "Assets/2. Enemy/Script/Data/EnemyStatData.csv";
	[MenuItem("Tools/Stat Data Import/Enemy Stat Import")]
	public static void ShowWindow()
	{
		GetWindow<EnemyStatDataImporter>("Enemy Data");
	}
	private void OnGUI()
	{
		GUILayout.Label("Enemy Data Importer", EditorStyles.boldLabel);
		csvPath = EditorGUILayout.TextField("CSV Path", csvPath);
		if(GUILayout.Button("Import Enemy Stat Data"))
		{
			ImportEnemyStatCSV();
		}
	}
	private void ImportEnemyStatCSV()
	{
		if(!File.Exists(csvPath))
		{
			Debug.LogError($"CSV 파일을 찾을 수 없습니다 : {csvPath}");
			return;
		}

		string[] lines = File.ReadAllLines(csvPath, System.Text.Encoding.UTF8);
		if (lines.Length < 2) return;

		string[] headers = lines[0].Trim().Split(',');

		for (int i = 1; i < lines.Length; i++)
		{
			string[] values = lines[i].Trim().Split(",");
			if (values.Length < headers.Length) continue;

			string enemyName = values[1];
			string folderPath = "Assets/Resources/Data/Enemy";
			string assetPath = $"{folderPath}/{enemyName}_Stat.asset";

			Directory.CreateDirectory(folderPath);

			EnemyStatData asset = AssetDatabase.LoadAssetAtPath<EnemyStatData>(assetPath);
			if (asset == null)
			{
				asset=ScriptableObject.CreateInstance<EnemyStatData>();
				AssetDatabase.CreateAsset(asset, assetPath);
			}

			Type type = typeof(EnemyStatData);
			for (int j = 0; j < headers.Length; j++)
			{
				string header = headers[j].Trim();

				FieldInfo field = type.GetField("_"+header,BindingFlags.Instance | BindingFlags.NonPublic);

				if(field != null)
				{
					object convertedValue = ConvertToValue(values[j].Trim(), field.FieldType);
					field.SetValue(asset, convertedValue);
				}

			}
			EditorUtility.SetDirty(asset);
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.DisplayDialog("완료", "Enemy Stat Data 임포트 완료", "확인");
			
		 
	}
	private object ConvertToValue(string value, Type targetType)
	{
		if (targetType == typeof(int)) return int.Parse(value);
		if (targetType == typeof(string)) return value;
		if (targetType == typeof(float)) return float.Parse(value);
		if (targetType.IsEnum) return Enum.Parse(targetType, value);

		return null;
	}
}
#endif
