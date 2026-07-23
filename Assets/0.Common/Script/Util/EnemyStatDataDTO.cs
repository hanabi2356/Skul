using System;
using System.Collections.Generic;
[Serializable]
public class EnemyStatDataDTO
{
    public string EnemyID;
	public string EnemyName;
    public string EnemyRarity;
    public int MaxHP;
    public string AttackType;
    public float AttackPower;
    public float AttackSpeed;
    public float AttackCoolTime;
    public float AttackRange;
    public float DetectedRange;
    public float MoveSpeed;

        

}

[Serializable]
public class EnemyStatTableDTO
{
    public List<EnemyStatDataDTO> EnemyStatDataList = new List<EnemyStatDataDTO>();
}
