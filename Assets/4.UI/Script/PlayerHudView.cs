using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHudView : MonoBehaviour, IPlayerHudView
{
	[SerializeField] private UIDocument _uiDocument;
	private ProgressBar _hpBar;
	private UnityEngine.UIElements.Label _goldLabel;

	public void Initialize()
	{
		if (_uiDocument == null || _uiDocument.rootVisualElement == null) return;

		_hpBar = _uiDocument.rootVisualElement.Q<ProgressBar>("PlayerHPBar");
		_goldLabel = _uiDocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("GoldValue");
	}

	public void SetGold(int currentGold)
	{
		if (_goldLabel == null) return;

		currentGold = Mathf.Max(0, currentGold);
		_goldLabel.text = currentGold.ToString();
	}

	public void SetHP(int currentHP, int maxHP)
	{
		if (_hpBar == null) return;

		maxHP = Mathf.Max(1, maxHP);
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);

		_hpBar.title = $"{maxHP} / {currentHP}";
		_hpBar.value = (float)currentHP / maxHP * 100.0f;
	}

	

}
