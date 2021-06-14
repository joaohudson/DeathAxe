using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CombatController))]
[RequireComponent(typeof(CharacterMotor))]
public class EnemieAI : StateMachine
{

    [SerializeField]
    private float detectRange = .6f;
    [SerializeField]
    private float stayAttackDuration = 2f;

    private CombatController combat;
    private CharacterMotor motor;
    private CharacterStats stats;
    private CharacterStats playerStats;
    private float attackRange;

    private IState attack;
    private IState followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CombatController>();
        motor = GetComponent<CharacterMotor>();
        playerStats = PlayerController.Instance.GetComponent<CharacterStats>();
        stats = GetComponent<CharacterStats>();
        stats.OnDeath += OnDeath;

        attackRange = combat.AttackRange;

        attack = new AttackState(motor, combat, stayAttackDuration);
        followPlayer = new FollowPlayerState(motor);
    }

    void OnDeath()
    {
        EnemieManager.Instance.RegisterDeath();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if ( stats.Stuned || stats.Died || playerStats.Died || GameState.Instance.GamePaused)
        {
            CurrentState = null;
            return;
        }

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 thisPos = transform.position;
        float distanceToPlayer = Vector3.Distance(playerPos, thisPos);

        if (distanceToPlayer <= attackRange)//condição de ataque
        {
            CurrentState = attack;
        }
        else if (distanceToPlayer <= detectRange)//condição de perseguição ao joagdor
        {
            CurrentState = followPlayer;
        }
        else//condição de inativo
        {
            CurrentState = null;
        }
    }
}
