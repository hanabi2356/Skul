using System;


/// <summary>
/// 현재는 골드 밖에 없지만 확장성을 고려하여 enum선언
/// </summary>
public enum ECurrencyType
{
	Gold
}

public interface ICurrencyModel 
{
	/// <summary>
	/// 재화 변화를 통지할 이벤트 
	/// </summary>
	public event Action<ECurrencyType, int> OnChangeCurrency;

	/// <summary>
	/// 현재 보유한 재화의 수를 반환
	/// </summary>
	/// <param name="type">재화 타입</param>
	/// <returns></returns>
	public int GetAmount(ECurrencyType type);


	/// <summary>
	/// 재화를 얻을 때 재화를 추가하는 함수
	/// </summary>
	/// <param name="type">재화 타입</param>
	/// <param name="amount">획득한 재화의 수</param>
	public void CurrencyAdd(ECurrencyType type, int amount);

	/// <summary>
	/// 재화 소비 함수 
	/// </summary>
	/// <param name="type">재화 타입</param>
	/// <param name="amount">소비할 재화의 수</param>
	/// <returns>제화가 부족하면 제화를 사용하지 않고false를 반환</returns>
	public bool TrySpend(ECurrencyType type, int amount);

	/// <summary>
	/// 데이터 로드 등으로 보유량을 직접 설정하는 함수
	/// 테스트에도 사용할 예정
	/// </summary>
	/// <param name="type">재화 타입</param>
	/// <param name="amount">설정할 보유량</param>
	public void SetAmount(ECurrencyType type, int amount);
}
