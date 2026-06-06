#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class MapExporter : EditorWindow
{
    /// <summary>
    /// 외부 파일로 추출할 맵 prefab 주입
    /// </summary>
    private GameObject exportMap; 

    [MenuItem("Tools/Map Exporter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MapExporter>("MapExport");
    }
    private void OnGUI()
    {
        GUILayout.Label("Map Exporter", EditorStyles.boldLabel);
        exportMap = EditorGUILayout.ObjectField("추출할 Map Prefab", exportMap, typeof(GameObject), false) as GameObject;
        if(GUILayout.Button("export"))
        {
            MapExport();
        }
    }

    private void MapExport()
    {
        if(exportMap != null)
        {
            Debug.Log("Export Test 성공");

        }
        else
        {
            Debug.LogError("Export 할 맵이 없음");
        }
    }
}
#endif
