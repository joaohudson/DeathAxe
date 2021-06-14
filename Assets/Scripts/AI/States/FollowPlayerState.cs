using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerState : IState
{
    private CharacterMotor motor;

    public FollowPlayerState(CharacterMotor motor)
    {
        this.motor = motor;
    }

    public void OnEnter(){}

    public void OnExit()
    {
        motor.Direction = Vector3.zero;
    }

    public void OnUpdate()
    {
        Vector3 direction = Util.DirectionToPlayer(motor.transform.position);
        motor.Direction = direction;
        motor.Orientation = direction;
    }
}
