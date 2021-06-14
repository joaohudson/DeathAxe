using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack")]
public class AttackInfo : ScriptableObject
{
    [SerializeField]
    private int attackID = 0;
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float knockback = 10f;
    [SerializeField]
    private float dash = 0f;
    [SerializeField]
    private float attackDuration = .5f;
    [SerializeField]
    private float attackDelay = .3f;
    [SerializeField]
    private float vibration = 0f;

    /// <summary>
    /// O dano causado por este ataque.
    /// </summary>
    public int Damage => damage;

    /// <summary>
    /// O knockback deste ataque.
    /// </summary>
    public float Knockback => knockback;

    /// <summary>
    /// Duração do ataque.
    /// </summary>
    public float AttackDuration => attackDuration;

    /// <summary>
    /// Tempo do início da animação até causar dano.
    /// </summary>
    public float AttackDelay => attackDelay;

    /// <summary>
    /// ID do ataque, único por ataque.
    /// </summary>
    public int AttackID => attackID;

    /// <summary>
    /// Fator de vibração da câmera ao acertar um alvo.
    /// </summary>
    public float Vibration => vibration;

    /// <summary>
    /// Avanço feito ao efetuar o ataque.
    /// </summary>
    public float Dash => dash;
}
