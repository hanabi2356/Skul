using UnityEngine;

public class SkulDataLoader 
{
	public SkulStatData SkulStatDataLoad(string name)
	{
		string path = "Data/Skul/" + name + "_Stat";
		SkulStatData data = Resources.Load<SkulStatData>(path);
		if (data == null)
		{
			Debug.LogError($"SkulStat 로딩 실패 [경로 : {path}]");
		}
		else
		{
			Debug.Log("Load 성공");
		}

		return data;
	}
}
