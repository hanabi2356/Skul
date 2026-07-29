#if UNITY_EDITOR
using Codice.Client.Common.GameUI;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FileOpenUtility : EditorWindow
{
	

	[MenuItem("Tools/FileOpen/CSV File Open")]
	public static void ShowWindow()
	{
		GetWindow<FileOpenUtility>("File Open");
	}
	private void OnGUI()
	{
		if(GUILayout.Button("CSV File Open"))
		{
			CSVFileOpen();
		}
	}
	private void CSVFileOpen()
	{
		string csvPath = "Assets/2.Enemy/Data/EnemyStatData.csv";
		TextAsset csvAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(csvPath);

		if (csvAsset != null)
		{
			AssetDatabase.OpenAsset(csvAsset);
		}
	}
}
#endif
