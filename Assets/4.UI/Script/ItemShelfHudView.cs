using UnityEngine;
using UnityEngine.UIElements;

public class ItemShelfHudView : MonoBehaviour
{
	[SerializeField] private UIDocument _uiDocument;

	private UnityEngine.UIElements.Label _priceLabel;

	private void LateUpdate()
	{
		
	}

	public void Initialize()
	{
		if(_uiDocument == null || _uiDocument.rootVisualElement == null) return;
		_priceLabel = _uiDocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("PriceLabel");
	}
	public void SetPrice(int price)
	{
		SetVisible(true);
		EnsureReady();
		if (_priceLabel == null) return;

		_priceLabel.text = Mathf.Max(0, price).ToString();
	}
	public void Hide()
	{
		SetVisible(false);
	}

	private void SetVisible(bool visible)
	{
		if (_uiDocument == null) return;
		_uiDocument.gameObject.SetActive(visible);
	}
	private void EnsureReady()
	{
		if (_priceLabel == null)
		{
			Initialize();
		}
	}

}
