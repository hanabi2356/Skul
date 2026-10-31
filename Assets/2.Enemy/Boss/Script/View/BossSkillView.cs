using System;
using UnityEngine;

[Serializable]
public struct MagicSpellSettings
{
	[SerializeField] private Transform _spellPoint;
	public Transform SpellPoint => _spellPoint;
}

public class BossSkillView : MonoBehaviour
{
	[SerializeField] private MagicSpellSettings _magicSpell;
	public MagicSpellSettings MagicSpell => _magicSpell;
}
