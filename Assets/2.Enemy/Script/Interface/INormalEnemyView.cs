using System;
using UnityEngine;

public interface INormalEnemyView
{
	public Transform NormalEnemyTransform { get; }
	public event Action OnAttack;
	public Rigidbody2D Rigidbody { get; }
	public Animator Animator { get; }
	public bool IsAttacking { get; }
	public NormalEnemyAnimEventListener NormalEnemyAnimEventListener { get; }
	public NormalEnemyPhysicsHandler PhysicsHandler { get; }
	public Vector2 TargetPosition { get; }
	public Vector2 Velocity { get; }
	public string ProjectileAddress { get; }
	public Transform ProjectileSpawnTransform { get; }
	public int ProjectileCount { get; }
	public bool AimAtTarget { get; }
	public bool UseHorizontialRangeOnly { get; }
	public void UpdateTargetPosition(Vector2 targetPosition);
	public void SetVelocity(float x, float y);
	public void SetVelocityX(float x);
	public void SetVelocityY(float y);
	public void SetIsAttacking(bool value);
	public void SetRotation(bool lookRight);
	
}
