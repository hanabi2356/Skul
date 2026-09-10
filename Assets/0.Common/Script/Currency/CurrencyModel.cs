using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CurrencyModel : ICurrencyModel
{
	public event Action<ECurrencyType, int> OnChangeCurrency;
	private Dictionary<ECurrencyType, int> _currencyAmounts = new Dictionary<ECurrencyType, int>();

	public CurrencyModel()
	{
		foreach(ECurrencyType type in Enum.GetValues(typeof(ECurrencyType)))
		{
			_currencyAmounts[type] = 0;
		}
	}

	public void CurrencyAdd(ECurrencyType type, int amount)
	{
		if (amount <= 0) return;

		_currencyAmounts[type] = (int)Mathf.Min((long)_currencyAmounts[type] + amount, int.MaxValue);
	}

	public int GetAmount(ECurrencyType type) => _currencyAmounts[type];
	

	public bool TrySpend(ECurrencyType type, int amount)
	{
		if(amount <= 0) return false;
		if (_currencyAmounts[type] < amount) return false;

		_currencyAmounts[type] -= amount;
		OnChangeCurrency?.Invoke(type, _currencyAmounts[type]);  

		return true;
	}

	public void SetAmount(ECurrencyType type, int amount)
	{
		int currency = Math.Max(amount, 0);
		if (_currencyAmounts[type] == currency) return;

		_currencyAmounts[type] = currency;
		OnChangeCurrency?.Invoke(type, _currencyAmounts[type]);
	}
}
