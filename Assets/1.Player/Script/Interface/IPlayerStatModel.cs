using System;
using UnityEngine;

public interface IPlayerStatModel
{
	//기본 정보
	public int CurrentHP { get; }
	public float FinalTakeDamageMultiply { get; }
	//공격력
	public float FinalPhysicsAttack { get; }
	public float FinalMagicAttack { get; }
	//공격, 이동, 정신집중 속도
	public float FinalAttackSpeed { get; }
	public float FinalMoveSpeed { get; }
	public float FinalConcentrationSpeed { get; }
	//쿨다운 속도
	public float FinalSkillCoolDownSpeed { get; }
	public float FinalSwapCoolDownSpeed { get; }
	public float FinalQuintessenceCoolDownSpeed { get; }
	//치명타 확률 및 치명타 데미지 배수
	public float FinalCriticalProbablility { get; }
	public float FinalCriticalDamageMultiply { get; }
	//점프
	public float FinalJumpForce { get; }
	public float FinalFallMultiply { get; }
	public int FinalJumpMaxCount { get; }
	//대쉬
	public float FinalDashForce { get; }
	public float FinalDashCoolTime { get; }
	public float FinalDashDuration { get; }
	public int FinalDashMaxCount { get; }
	//공격
	public float FinalAttackCountResetDelay { get; }
	public int FinalMaxAttackCount { get; }
	public float FinalInputBufferTime { get; }

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
