#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SkulStatDataImporter : EditorWindow
{
    private string csvPath = "Assets/1.Player/Data/SkulStatData.csv";
    [MenuItem("Tools/Stat Data Import/Skul Stat Import")]
    public static void ShowWindow()
    {
        GetWindow<SkulStatDataImporter>("Skul Data");
    }
    private void OnGUI()
    {
        GUILayout.Label("Skul Data Importer", EditorStyles.boldLabel);
        csvPath = EditorGUILayout.TextField("CSV Path", csvPath);
        if (GUILayout.Button("Import Skul Stat Data"))
        {
            ImportSkulStatCSV();
        }
    }
    private void ImportSkulStatCSV()
    {
        if (!File.Exists(csvPath)) 
        {
            Debug.LogError($"CSV 파일을 찾을 수 없습니다 : {csvPath}");
            return;
        }

        //모든 라인 읽어들이기
        string[] lines = File.ReadAllLines(csvPath,System.Text.Encoding.UTF8);
        if (lines.Length < 2)
            return;

        //header 자르기
        string[] headers = lines[0].Trim().Split(',');

        //lines[0]은 헤더이기 때문에 i=1부터 시작해야 한다
        for (int i = 1; i < lines.Length; i++)
        {
            //값 자르기
            string []values = lines[i].Trim().Split(",");
            if (values.Length < headers.Length)
                continue;

            //csv의 이름 칸을 기반으로 파일을 찾거나 만든다
            string skulName = values[1];
			//저장위치 
            string assetPath = $"Assets/Resources/Data/Skul/{skulName}_Stat.asset";

            //폴더 자동 생성
            Directory.CreateDirectory("Assets/Resources/Data/Skul");

            SkulStatData asset = AssetDatabase.LoadAssetAtPath<SkulStatData>(assetPath);
            if(asset==null)
            {
                asset=ScriptableObject.CreateInstance<SkulStatData>();
                AssetDatabase.CreateAsset(asset, assetPath);
            }

            //리플렉션을 이용한 데이터 주입
            Type type = typeof(SkulStatData);
            for (int j = 0; j < headers.Length; j++)
            {
                string header=headers[j].Trim();

                //실제 ScriptableObject 데이터 접근
                FieldInfo field = type.GetField("_" + header, BindingFlags.Instance | BindingFlags.NonPublic);

                if(field != null)
                {
                    object convertedValue = ConvertToValue(values[j].Trim(),field.FieldType);
                    field.SetValue(asset,convertedValue);
                }
            }
            //변경된 데이터를 디스크에 물리적으로 기록
            EditorUtility.SetDirty(asset);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("완료", "Skul Stat Data 임포트 완료", "확인");
        

    }
    private object ConvertToValue(string value, Type targetType)
    {
        if(targetType == typeof(int)) return int.Parse(value);
        if (targetType == typeof(string)) return value;
        if (targetType == typeof(float)) return float.Parse(value);
        if (targetType.IsEnum) return Enum.Parse(targetType, value, true);
        return null;
    }

}
#endif
