using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CombatController))]
public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private CharacterMotor motor;
    private CombatController combat;
    private CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        motor = GetComponent<CharacterMotor>();
        combat = GetComponent<CombatController>();

        stats.OnDeath += OnDeath;
        stats.OnDamage += OnDamage;
    }

    private void OnDamage(DamageEvent e)
    {
        combat.CancelAttack();
        motor.Direction = Vector3.zero;
    }

    private void OnDeath()
    {
        motor.Direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.GamePaused || stats.Stuned || stats.Died) 
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (Input.GetKey(KeyCode.J))
        {
            combat.Attack();
        }

        if (combat.Attacking)//se está atacando, não faz mais nada
        {
            motor.Direction = Vector3.zero;
            return;
        }

        motor.Direction = direction;

        if (direction.sqrMagnitude > 0f)
        {
            motor.Orientation = direction;
        }

        if (Input.GetButtonDown("Jump"))
        {
            motor.Jump();
        }
    }
}
