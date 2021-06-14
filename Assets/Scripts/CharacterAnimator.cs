using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(CharacterStats)))]
public class CharacterAnimator : MonoBehaviour
{
    public const string ATTACK_KEY = "Attack";
    public const string WALK_KEY = "Walk";
    public const string IDLE_KEY = "Idle";
    public const string STUN_KEY = "Hited";
    public const string DIE_KEY = "Die";
    public const string ATTACK_ID_KEY = "AttackID";

    [SerializeField]
    private Animator animator;

    private CharacterStats stats;

    private void Start()
    {
        var combat = GetComponent<CombatController>();
        combat.OnAttack += OnAttack;

        stats = GetComponent<CharacterStats>();
        stats.OnDeath += OnDeath;
        stats.OnStun += OnStun;
    }

    private void OnStun()
    {
        animator.SetTrigger(STUN_KEY);
    }

    private void OnDeath()
    {
        animator.SetBool(DIE_KEY, true);
    }

    private void OnAttack(AttackInfo attack)
    {
        animator.SetInteger(ATTACK_ID_KEY, attack.AttackID);
        animator.SetTrigger(ATTACK_KEY);
    }

    public float Speed {
        set => animator.SetFloat("Speed", value);
    }
}
