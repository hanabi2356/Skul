using System;
using UnityEngine;

public interface INormalEnemyView
{
	Transform NormalEnemyTransform { get; }
	event Action OnAttack;
	Rigidbody2D Rigidbody { get; }
	Animator Animator { get; }
	bool IsAttacking { get; }
	NormalEnemyAnimEventListener NormalEnemyAnimEventListener { get; }
	NormalEnemyPhysicsHandler PhysicsHandler { get; }
	Vector2 TargetPosition { get; }
	Vector2 Velocity { get; }
	void UpdateTargetPosition(Vector2 targetPosition);
	void SetVelocity(float x, float y);
	void SetVelocityX(float x);
	void SetVelocityY(float y);
	void SetIsAttacking(bool value);
	void SetRotation(bool lookRight);
}
