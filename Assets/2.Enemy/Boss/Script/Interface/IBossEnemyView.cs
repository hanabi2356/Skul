using System;
using UnityEngine;

public interface IBossEnemyView 
{
	public Transform BossEnemyTransform { get; }
	public event Action OnSkill;
	public Animator Animator { get; }
	public Rigidbody2D Rigidbody { get; }
	public bool UseSkill { get; }
	public EnemyPhysicsHandler PhysicsHandler { get; }
	public Vector2 TargetPosition { get; }
	public BossEnemyAnimEventListener AnimEventListener { get; }
	public LayerMask TargetLayer { get; }
	public int CurrentPhase { get; }
	public bool LookRight { get; }
	public float LookDirection { get; }
	public void SetUseSkill(bool useSkill);
	public void SetRotation(bool lookRight);
	public void UpdateTargetPosition(Vector2 targetPosition);
	public void SetPhase(int phase);

}
