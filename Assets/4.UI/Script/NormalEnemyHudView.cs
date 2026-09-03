using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalEnemyHudView : MonoBehaviour, INormalEnemyHudView
{
	[SerializeField] private UIDocument _uiDocument;
	[SerializeField, Label("Progress Bar 출력 시간")] private float _progressBarActivateTime = 2.0f;
	[SerializeField] private Transform _rotationRoot;
	private ProgressBar _hpBar;

	private Coroutine _hideCo;
	private WaitForSeconds _progressBarWait;

	private Transform RotationTarget => _uiDocument != null ? _uiDocument.transform : null;
	private void LateUpdate()
	{
		if (_rotationRoot == null) return;

		RotationTarget.rotation = Quaternion.identity;
	}
	public void Initialize()
	{
		if (_uiDocument == null || _uiDocument.rootVisualElement == null) return;

		_hpBar = _uiDocument.rootVisualElement.Q<ProgressBar>("NormalEnemyHPBar");
	}

	
	private void SetHP(int currentHP, int maxHP)
	{
		if (_hpBar == null) return;

		maxHP = Mathf.Max(1, maxHP);
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);

		_hpBar.value = (float)currentHP / maxHP * 100.0f;
	}

	public void Show(int currentHP, int maxHP)
	{
		EnsureReady();

		SetHP(currentHP, maxHP);
		SetVisible(true);

		if(_hideCo != null)
		{
			StopCoroutine(_hideCo);
		}

		_hideCo = StartCoroutine(IEHideRoutine());
	}

	public void Hide()
	{
		if(_hideCo != null)
		{
			StopCoroutine(_hideCo);
			_hideCo = null;
		}
		SetVisible(false);
	}

	private void SetVisible(bool visible)
	{
		if (_uiDocument == null) return;
		_uiDocument.gameObject.SetActive(visible);
	}

	private IEnumerator IEHideRoutine()
	{
		yield return _progressBarWait;
		SetVisible(false);
		_hideCo = null;

	}
	

	/// <summary>
	/// WaitForSeconds의 생성 타이밍이 안맞을 경우를 위한 함수
	/// </summary>
	private void EnsureReady()
	{
		if(_progressBarWait == null)
		{
			_progressBarWait = new WaitForSeconds(_progressBarActivateTime);
		}

		if(_hpBar == null)
		{
			Initialize();
		}
	}

}
