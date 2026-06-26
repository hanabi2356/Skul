using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView 
{
	public PlayerPhysicsHandler PhysicsHandler { get; }

	public event Action<Vector2> OnMove;
	public event Action OnJump;
	public event Action OnDash;
	public event Action OnAttack;
	public Rigidbody2D Body { get; }
	public float CurrentVelocityY { get; }

	public void SetVelocityX(float x);
	public void SetVelocityY(float y);
	public void SetVelocity(float x, float y);
	public void SetRotation(bool lookRight);
	public void SetGravityScale(bool isDash);
	public void AddVelocity(Vector3 velocity);
	public void SetOneWayPlatformCollision(bool ignore);
	public void InputMoveVector(InputAction.CallbackContext context);
	public void InputJump(InputAction.CallbackContext context);
	public void InputDash(InputAction.CallbackContext context);
	public void InputAttack(InputAction.CallbackContext context);
}
