using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView 
{
	/// <summary>
	/// 물리 충돌 조건을 위한 물리 핸들러
	/// </summary>
	public PlayerPhysicsHandler PhysicsHandler { get; }
	public Rigidbody2D Rigidbody { get; }
	public Animator Animator { get; }
	public PlayerAnimEventListener PlayerAnimEventListener { get; }
	public Transform PlayerTransform { get; }

	public event Action<Vector2> OnMove;
	public event Action OnJump;
	public event Action OnDash;
	public event Action OnAttack;
	public event Action OnPlatformIgnore;
	public float CurrentVelocityY { get; }
	public bool IsAttacking { get; }
	public bool CanAttackDash { get; }
	public bool IsHit { get; }
	/// <summary>
	/// player의 linearVelocity.x 값 변경
	/// </summary>
	/// <param name="x"></param>
	public void SetVelocityX(float x);

	/// <summary>
	/// player의 linearVelocity.y 값 변경
	/// </summary>
	/// <param name="y"></param>
	public void SetVelocityY(float y);

	/// <summary>
	/// player의 linearVelocity.x, y 값 변경
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetVelocity(float x, float y);

	/// <summary>
	/// player의 시선의 따른 rotation값 변경
	/// </summary>
	/// <param name="lookRight">오른쪽을 보고 있는지에 대한 여부</param>
	public void SetRotation(bool lookRight);

	/// <summary>
	/// 대시 상태에 따른 중력 수치 변경
	/// </summary>
	/// <param name="isDash">true: 0/ false: Rigidbody2D에 지정한 GravityScale</param>
	public void SetGravityScale(bool isDash);

	public void AddImpulse(Vector2 impulse);
	public void AttackDash(float force);

	/// <summary>
	/// OneWayPlatform 무시 여부 결정
	/// </summary>
	/// <param name="ignore">무시 여부</param>
	public void SetOneWayPlatformCollision(bool ignore);
	public void InputMoveVector(InputAction.CallbackContext context);
	public void InputJump(InputAction.CallbackContext context);
	public void InputDash(InputAction.CallbackContext context);
	public void InputAttack(InputAction.CallbackContext context);
	public void InputPlatformIgnore(InputAction.CallbackContext context);
	public void SetIsAttacking(bool value);
	public void SetCanAttackDash(bool value);
	public void SetIsHit(bool value);
}
