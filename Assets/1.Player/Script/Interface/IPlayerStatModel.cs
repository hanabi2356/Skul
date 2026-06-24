using System;
using UnityEngine;

public interface IPlayerStatModel
{
	//기본 정보
	public int currentHP { get; }
	public float finalTakeDamageMultiply { get; }
	//공격력
	public float finalPhysicsAttack { get; }
	public float finalMagicAttack { get; }
	//공격, 이동, 정신집중 속도
	public float finalAttackSpeed { get; }
	public float finalMoveSpeed { get; }
	public float finalConcentrationSpeed { get; }
	//쿨다운 속도
	public float finalSkillCoolDownSpeed { get; }
	public float finalSwapCoolDownSpeed { get; }
	public float finalQuintessenceCoolDownSpeed { get; }
	//치명타 확률 및 치명타 데미지 배수
	public float finalCriticalProbablility { get; }
	public float finalCriticalDamageMultiply { get; }
	//점프
	public float finalJumpForce { get; }
	public float finalFallMultiply { get; }
	public int finalJumpMaxCount { get; }
	//대쉬
	public float finalDashForce { get; }
	public float finalDashCoolTime { get; }
	public float finalDashDuration { get; }
	public int finalDashMaxCount { get; }

	/// <summary>
	/// Hp 변경 시 통지할 이벤트
	/// </summary>
	public event Action<int> OnChangeHp;
	/// <summary>
	/// stat 업데이트가 완료 되었을 때 통지할 이벤트
	/// </summary>
	public event Action OnStatCaculated;
	/// <summary>
	/// stat 업데이트 함수
	/// </summary>
	/// <param name="defaultStatData">기본 공통 stat</param>
	/// <param name="currentSkulStatData">skul 별 개별 stat </param>
	public void UpdateFinalStat(DefaultStatData defaultStatData, SkulStatData currentSkulStatData);
	/// <summary>
	/// 피격 시 호출할 함수
	/// </summary>
	/// <param name="damage">피격 받은 데미지</param>
	public void TakeDamage(int damage);

	/// <summary>
	/// 해당 skul에 statData를 로드 하는 함수
	/// </summary>
	/// <param name="currentSkulStatData">data를 로드할 skul data파일</param>
	public void SetSkulStatData(SkulStatData currentSkulStatData);

}
