using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterMotor))]
public class CombatController : MonoBehaviour
{

    /// <summary>
    /// Chamado ao efetuar um ataque.
    /// </summary>
    public event Action<AttackInfo> OnAttack;

    [SerializeField]
    private AttackInfo[] attacks;
    [SerializeField]
    private float attackArea = .3f;
    [SerializeField]
    private string target;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private TrailRenderer effect;

    private CharacterMotor motor;

    private float nextAttackTime = .3f;
    private float hitTime = 0f;
    private float attackTime = 0f;
    private int nextAttack = 0;
    private int currnetAttack = 0;

    private void Start()
    {
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        if (Attacking)
        {
            //Contategm de tempo para hit
            if (hitTime > 0f)
            {
                hitTime -= Time.deltaTime;

                if (hitTime <= 0f)
                {
                    DoHit();
                }
            }
            //Duração do ataque
            if (attackTime > 0f)
            {
                attackTime -= Time.deltaTime;

                if (attackTime <= 0f)
                {
                    CancelAttack();
                }
            }
        }
        else
        {
            //Contagem de tempo para combo
            if (nextAttackTime > 0f)
            {
                nextAttackTime -= Time.deltaTime;

                if (nextAttackTime <= 0f)
                {
                    nextAttack = 0;
                }
            }
        }
    }

    private void DoHit()
    {
        AttackInfo attack = attacks[currnetAttack];
        var cds = Physics.OverlapSphere(attackPoint.position, attackArea);

        foreach (var cd in cds)
        {
            if (cd.CompareTag(target))
            {
                CameraController.Instance.Vibrate(attack.Vibration);
                var stats = cd.GetComponent<CharacterStats>();
                var motor = cd.GetComponent<CharacterMotor>();
                float dieFactor;
                stats.TakeDamage(attack.Damage, gameObject);
                dieFactor = stats.Health == 0 ? 2f : 1f;
                motor.AddKnockback(transform.forward * attack.Knockback * dieFactor);
                motor.Orientation = Util.DirectionTo(stats.transform, transform);
            }
        }

        this.motor.Dash(transform.forward * attack.Dash);
    }

    /// <summary>
    /// <summary>
    /// Cancela o ataque.
    /// </summary>
    public void CancelAttack()
    {
        attackTime = 0f;
        if(effect != null)
        {
            effect.emitting = false;
        }
    }

    /// Faz este personagem atacar.
    /// </summary>
    public void Attack()
    {
        if (Attacking)
            return;
        
        currnetAttack = nextAttack;
        
        AttackInfo attack = attacks[currnetAttack];

        OnAttack?.Invoke(attack);

        hitTime = attack.AttackDelay;
        attackTime = attack.AttackDuration;

        if(effect != null)
        {
            effect.emitting = true;
        }

        nextAttackTime = .3f;
        if(nextAttackTime > 0f)
        {
            nextAttack = (nextAttack + 1) % attacks.Length;
        }
    }

    /// <summary>
    /// Alcance do ataque.
    /// </summary>
    public float AttackRange
    {
        get {
            float d = Vector3.Distance(attackPoint.position, transform.position);
            return d + attackArea;
        }
    }

    /// <summary>
    /// Se este personagem está atacando.
    /// </summary>
    public bool Attacking => attackTime > 0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
    }
}
