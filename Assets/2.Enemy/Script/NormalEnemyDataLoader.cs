using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NormalEnemyDataLoader
{
	private Dictionary<string, EnemyStatDataDTO> _statData = new Dictionary<string, EnemyStatDataDTO>();

	public void EnemyStatDataToCache()
	{
		if(_statData.Count > 0) return;

		string filePath = Path.Combine(Application.streamingAssetsPath, "EnemyStatTable.json");
		if(!File.Exists(filePath))		
		{
			Debug.LogError($"Json 파일 로드 실패 : {filePath}");
			return;
		}

		string json = File.ReadAllText(filePath);
		EnemyStatTableDTO table = JsonUtility.FromJson<EnemyStatTableDTO>(json); //직렬화된 파일을 역직렬화 한다
		if(table == null || table.EnemyStatDataList == null)
		{
			Debug.LogError($"Json 파일 파싱 실패 : {filePath}");
			return;
		}
		foreach(var data in table.EnemyStatDataList)
		{
			if(string.IsNullOrEmpty(data.EnemyID)) //key 존재 여부 검사
			{
				Debug.LogWarning($"EnemyID가 비어있는 데이터 건너뜁니다 : {data.EnemyID}");
				continue;
			}
			if(_statData.ContainsKey(data.EnemyID)) //중복 키 검사
			{
				Debug.LogWarning($"이미 존재하는 EnemyID 데이터 건너뜁니다 : {data.EnemyID}");
			}
			_statData.Add(data.EnemyID, data);

			Debug.Log($"EnemyID: {data.EnemyID} 데이터 로드 완료");
		}
	}
	public EnemyStatDataDTO Get(string enemyID)
	{
		if(_statData.TryGetValue(enemyID, out var data))
		{
			return data;
		}
		Debug.LogError($"EnemyID: {enemyID} 데이터 존재하지 않습니다");
		return null;
	}

}
