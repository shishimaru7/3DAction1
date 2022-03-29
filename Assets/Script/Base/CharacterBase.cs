using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵味方の基底クラス
/// </summary>
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected int hp = 100;
    public int Hp { set { hp = value; hp = Mathf.Clamp(hp, 0, 100); } get { return hp; } }

    [Header("最大HP")]
    [SerializeField]
    protected int maxHp;

    [Header("攻撃力")]
    [SerializeField]
    protected int attackPower;
    public int AttackPower { set { attackPower = value; attackPower = Mathf.Clamp(attackPower, 0, 100); } get { return attackPower; } }

    [Header("最大攻撃力")]
    [SerializeField]
    protected int attackMaxPower = 100;

    [Header("防御力")]
    [SerializeField]
    protected int defensePower;
    public int DefensepPower { set { defensePower = value; defensePower = Mathf.Clamp(defensePower, 0, 100); } get { return defensePower; } }
    [Header("最大防御力")]
    [SerializeField]
    protected int defenseMaxPower = 100;
}
