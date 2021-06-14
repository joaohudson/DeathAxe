using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private CharacterMotor motor;
    private CombatController combat;
    private float stayAttackDuration;
    private float stayAttack;

    public AttackState(CharacterMotor motor, CombatController combat, float stayAttackDuration)
    {
        this.motor = motor;
        this.combat = combat;
        this.stayAttackDuration = stayAttackDuration;
        this.stayAttack = stayAttackDuration;
    }

    public void OnEnter(){}

    public void OnExit(){
        combat.CancelAttack();
    }

    public void OnUpdate()
    {
        motor.Orientation = Util.DirectionToPlayer(motor.transform.position);

        if(stayAttack > 0)
        {
            stayAttack -= Time.deltaTime;
            return;
        }

        stayAttack = stayAttackDuration;
        combat.Attack();
    }
}
