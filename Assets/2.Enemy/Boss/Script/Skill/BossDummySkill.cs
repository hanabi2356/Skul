/// <summary>
/// Attack → Idle 완료 시그널 검증용 더미 스킬.
/// 실제 판정/이동 없이 Duration 타이머만으로 종료한다.
/// </summary>
public class BossDummySkill : SkillBase
{
	public BossDummySkill(IBossEnemyView view, IEnemyStatModel statModel)
		: base(view, statModel)
	{
	}

	protected override void OnBegin()
	{
		FaceTarget();
	}
}
