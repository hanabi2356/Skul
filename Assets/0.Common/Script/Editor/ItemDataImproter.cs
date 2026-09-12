#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
public class ItemDataImproter : EditorWindow
{
	private string _csvPath = "Assets/6.Item/Data/ItemsData.csv";

	[MenuItem("Tools/Stat Data Import/Item Stat Data")]
	public static void ShowWindow()
	{
		GetWindow<ItemDataImproter>("Item Data Import");
	}
	private void OnGUI()
	{
		GUILayout.Label("Item Data Import", EditorStyles.boldLabel);
		_csvPath = EditorGUILayout.TextField("CSV Path", _csvPath);
		if(GUILayout.Button("Item Data Import"))
		{
			ImportItemData();
		}
	}

	private void ImportItemData()
	{
		if (File.Exists(_csvPath) == false)
		{
			EditorUtility.DisplayDialog("Item Data Import", $"CSV 파일이 {_csvPath}에 존재 하지 않음", "OK");
			return;
		}

		string[] lines = File.ReadAllLines(_csvPath, System.Text.Encoding.UTF8);
		if (lines.Length < 2)
		{
			EditorUtility.DisplayDialog("Item Data Import", "Data 미입력", "OK");
			return;
		}

		string[] headers = lines[0].Trim().Split(',');

		for (int i = 1; i < lines.Length; i++)
		{
			string[] values = lines[i].Trim().Split(',');
			if (values.Length < headers.Length) continue;

			string itemName = values[1];
			string folderPath = "Assets/Resources/Data/Item";
			string assetPath = $"{folderPath}/{itemName}_stat.asset";

			Directory.CreateDirectory(folderPath);

			ItemData asset = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);

			if(asset == null)
			{
				asset = ScriptableObject.CreateInstance<ItemData>();
				AssetDatabase.CreateAsset(asset, assetPath);
			}

			Type type = typeof(ItemData);
			for(int j=0; j<headers.Length; j++)
			{
				string header = headers[j].Trim();

				FieldInfo info = type.GetField("_" + header, BindingFlags.Instance | BindingFlags.NonPublic);

				if (info != null)
				{
					object convertValue = ConvertToValue(values[j], info.FieldType);
					info.SetValue(asset, convertValue);

				}
			}
			EditorUtility.SetDirty(asset);
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.DisplayDialog("Item Data Import", "Import 완료", "OK");
	}

	private object ConvertToValue(string value, Type targetType)
	{
		if(targetType == typeof(int)) return int.Parse(value);
		if (targetType == typeof(string)) return value;
		if (targetType == typeof(float)) return float.Parse(value);
		if(targetType.IsEnum) return Enum.Parse(targetType, value);
		

		return null;
	}


}
#endif
