using System;
using UnityEngine;

[Serializable]
public struct MagicSpellSettings
{
	[SerializeField] private Transform _spellPoint;
	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _centerPrefab;
	public  readonly Transform SpellPoint => _spellPoint;
	public readonly GameObject LaserPrefab => _laserPrefab;
	public readonly GameObject CenterPrefab => _centerPrefab;

}

public class BossSkillView : MonoBehaviour
{
	[SerializeField] private MagicSpellSettings _magicSpell;
	public MagicSpellSettings MagicSpell => _magicSpell;
}
