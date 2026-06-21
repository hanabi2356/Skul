#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[Serializable]
public class TileData
{
    public int x;
    public int y;
    public string tileName;
    public string tileMapName;
}

[Serializable]
public class TileMapSaveData
{
    public List<TileData> tiles = new List<TileData>();
}

public class MapExporter : EditorWindow
{
    /// <summary>
    /// 외부 파일로 추출할 맵 prefab 주입
    /// </summary>
    private GameObject exportMap;

    private int _doorMaxCount = 0;
    
    
    [MenuItem("Tools/Map Exporter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MapExporter>("MapExport");
    }
    private void OnGUI()
    {
        GUILayout.Label("Map Exporter", EditorStyles.boldLabel);
        exportMap = EditorGUILayout.ObjectField("추출할 Map Prefab", exportMap, typeof(GameObject), false) as GameObject;

        GUILayout.Label("Map Object Setting", EditorStyles.boldLabel);
        _doorMaxCount = EditorGUILayout.IntField("문의 최대 개수", _doorMaxCount);
        if(GUILayout.Button("export"))
        {
            MapExport(exportMap);
        }
    }

    private void MapExport(GameObject map)
    {

        if(map == null)
        {
            EditorUtility.DisplayDialog("맵 추출 실패", "Prefab 지정 안함", "확인");
            return;
        }


        TileMapSaveData saveData = new TileMapSaveData();
        Tilemap[] tilemap = map.GetComponentsInChildren<Tilemap>();

        //맵 유효성 검사 용 변수
        int totalTileCount = 0;
        int doorCount = 0;
        int spawnPosCount = 0;

        for (int i = 0; i < tilemap.Length; i++)
        {
            Tilemap currentTilemap = tilemap[i];
            BoundsInt bounds = currentTilemap.cellBounds;

            foreach(var pos in bounds.allPositionsWithin)
            {
                if (currentTilemap.HasTile(pos))
                {
                    TileBase tile = currentTilemap.GetTile(pos);
                    totalTileCount++;

                    if(tile.name== "DoorPosition")
                        doorCount++;
                    if(tile.name == "SpawnPosition")
                        spawnPosCount++;

                    TileData data = new TileData();

                    data.x = pos.x;
                    data.y = pos.y;
                    data.tileName = tile.name;
                    data.tileMapName = currentTilemap.name;


                    saveData.tiles.Add(data);
                }
            }
        }

        //맵 유효성 검사용 조건문

        //맵에 배치된 타일이 있는지 검사
        if (totalTileCount == 0)
        {
            
            EditorUtility.DisplayDialog("맵 추출 실패", "사유: Tile 배치 안함", "확인");
            return;
        }

        //문이 최대 개수 이하 0개 이상 배치 되어있는지 검사
        if(doorCount > _doorMaxCount)
        {
            EditorUtility.DisplayDialog("맵 추출 실패", "사유: Door 과배치, Door 배치 수: {doorCount}", "확인");
            return;
        }
        else if(doorCount == 0)
        {
      
            EditorUtility.DisplayDialog("맵 추출 실패", "사유: Door 배치 안함", "확인");
            return;
        }

        //spawnPosition이 1개 인지 검사
        if(spawnPosCount >= 2)
        {
            EditorUtility.DisplayDialog("맵 추출 실패", $"사유: SpawnPosition 1개 초과 배치, SpawnPosition 배치 수: {spawnPosCount}", "확인");
            
            return;
        }
        else if(spawnPosCount == 0)
        {
            EditorUtility.DisplayDialog("맵 추출 실패", "사유: SpawnPosition 배치 안함", "확인");
            return;
        }


        string folderPath = Application.streamingAssetsPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }

        string fileName = $"{map.name}_map.json";
        string filePath = Path.Combine(folderPath, fileName);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Map Export 성공");
        
        AssetDatabase.Refresh();
        
    }
}
#endif
