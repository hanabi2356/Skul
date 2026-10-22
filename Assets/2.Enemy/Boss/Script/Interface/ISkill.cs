
using UnityEngine;

public interface ISkill
{
    public int SkillNumber { get; }
	public bool CanUse { get; }
	public bool IsRunning { get; }
	public float MaxUseRange { get; }
	public bool IsEndRequested { get; }
	public void Enter();
	public void Tick();
	public void Exit();
	public void OnHit();
	public void Cancle();
}
