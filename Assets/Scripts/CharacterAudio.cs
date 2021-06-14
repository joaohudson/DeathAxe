using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip hitedSound;
    [SerializeField]
    private AudioClip diedSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip bloodSound;
    // Start is called before the first frame update
    void Start()
    {
        var stats = GetComponent<CharacterStats>();
        var combat = GetComponent<CombatController>();
            
        stats.OnDamage += OnDamage;
        stats.OnDeath += OnDeath;
        combat.OnAttack += OnAttack;
    }

    private void OnAttack(AttackInfo attack)
    {
        source.clip = attackSound;
        source.Play();
    }

    private void OnDeath()
    {
        source.clip = diedSound;
        source.Play();
    }

    private void OnDamage(DamageEvent e)
    {
        //source.clip = hitedSound;
        //source.Play();
    }
}
