using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseUIView : MonoBehaviour, IPauseUIView
{
	[SerializeField] private UIDocument _uiDocument;
	[SerializeField] private PlayerInput _playerInput;

	private VisualElement _pauseRoot;
	private bool _isPaused;

	private IEnumerator Start()
	{
		yield return null;
		Initialize();
	}

	public void Initialize()
	{
		if (_uiDocument == null || _uiDocument.rootVisualElement == null) return;

		var root = _uiDocument.rootVisualElement;
		root.style.width = Length.Percent(100);
		root.style.height = Length.Percent(100);

		_pauseRoot = root.Q<VisualElement>("PauseRoot");
		SetVisible(false);
	}

	public void SetVisible(bool visible)
	{
		if (_pauseRoot == null) return;

		_pauseRoot.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
	}

	/// <summary>
	/// GlobalInputManager PlayerInput → UI/Pause 에 연결
	/// </summary>
	public void InputPause(InputAction.CallbackContext context)
	{
		if (!context.started) return;

		TogglePause();
	}

	private void TogglePause()
	{
		if (_isPaused)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	private void Pause()
	{
		_isPaused = true;
		Time.timeScale = 0f;
		SetVisible(true);
		_playerInput?.actions.FindActionMap("Player").Disable();
	}

	private void Resume()
	{
		_isPaused = false;
		Time.timeScale = 1f;
		SetVisible(false);
		_playerInput?.actions.FindActionMap("Player").Enable();
	}

	private void OnDisable()
	{
		if (!_isPaused) return;

		Time.timeScale = 1f;
		_isPaused = false;
	}
}
