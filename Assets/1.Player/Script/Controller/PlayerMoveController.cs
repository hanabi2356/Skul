using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;


public class PlayerMoveController 
{
    private IPlayerStatModel _statModel;
	private IPlayerView _view;

	public Vector2 MoveInput { get; private set; }
	public Vector2 GazeVector { get; private set; } = new Vector2(1.0f, 0.0f); //시선 백터

	private int _jumpCount;
	private bool _isJump = true;
	private int _dashCount=0;
	public bool IsDashing { get; private set; } = false;
	private bool _isCoyoteTimeEnd;
	private bool _canMove = true;
	private bool _isDashCoolDown;
    
	public Func<int> GetAttackCount { get; set; }
    
    public PlayerMoveController(IPlayerStatModel statModel, IPlayerView view)
	{
		_statModel = statModel;
		_view = view;
	}

    public void TryJump()
	{
		if(_isJump && _jumpCount < _statModel.FinalJumpMaxCount)
		{
			_view.SetVelocityY(_statModel.FinalJumpForce);
			_jumpCount++;
		}
	}
	public void TryDash()
	{
		if(!IsDashing && _dashCount < _statModel.FinalDashMaxCount)
		{
			_ = StartDash();
		}
	}

	private async Task StartDash()
	{
		IsDashing = true;
		_dashCount++;

		_view.SetGravityScale(true);

		float dashX = GazeVector.x * (_statModel.FinalDashForce + Mathf.Abs(MoveInput.x));
		_view.SetVelocity(dashX, 0.0f);

		await Task.Delay((int)(_statModel.FinalDashDuration * 1000));

		IsDashing = false;
		_view.SetGravityScale(false);
		_view.SetVelocity(0.0f, 0.0f);

		if(!_isDashCoolDown)
		{
			_ = StartDashCoolDown();
		}
	}

	private async Task StartDashCoolDown()
	{
		_isDashCoolDown = true;

		while (_dashCount > 0)
		{
			await Task.Delay((int)(_statModel.FinalDashCoolTime * 1000));
			_dashCount = 0;
		}

		_isDashCoolDown = false;
	}

	public void SetMoveInput(Vector2 moveInput)
	{
		MoveInput = moveInput;
		if (MoveInput != Vector2.zero)
		{
			GazeVector = MoveInput;
		}
	}

	public void FixedTick()
	{
		if (_view == null) return;

		if (IsDashing) return;

		bool ignore = _view.CurrentVelocityY > 0.1f;
		_view.SetOneWayPlatformCollision(ignore);

		bool lookRight = GazeVector.x >= 0.0f;
		bool isWall = _view.PhysicsHandler.IsWallCheck(lookRight);


		if (!isWall)
		{
			float targetX = MoveInput.x * _statModel.FinalMoveSpeed;
			_view.SetVelocityX(targetX);
		}
		else
		{
			_view.SetVelocityX(0.0f);
		}

		_view.SetRotation(lookRight);
		if (_view.PhysicsHandler.IsGround() && _view.CurrentVelocityY <= 0.1f)
		{
			_jumpCount = 0;
			_isJump = true;
		}

		if (_jumpCount >= _statModel.FinalJumpMaxCount)
		{
			_isJump = false;
		}
	}
}
