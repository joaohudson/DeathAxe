using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct DamageEvent
{
    /// <summary>
    /// Dano causado.
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// Objeto que causou o dano.
    /// </summary>
    public GameObject Owner { get; set; }
}

public class CharacterStats : MonoBehaviour
{
    /// <summary>
    /// Duração do atordoamento ao receber um ataque.
    /// </summary>
    private const float STUN_DURARION = .5f;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private GameObject damageEffect;
    [SerializeField]
    private GameObject fireEffect;

    private void Start()
    {
        Health = MaxHealth;
        Stuned = false;
    }

    /// <summary>
    /// Chamado sempre quando a vida deste personagem
    /// mudar.
    /// </summary>
    public event Action OnChangeHealth;

    /// <summary>
    /// Chamado quando a vida deste personagem zerar.
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    /// Chamado quando este personagem receber dano.
    /// </summary>
    public event Action<DamageEvent> OnDamage;

    /// <summary>
    /// Chamado quando este personagem for atordoado.
    /// </summary>
    public event Action OnStun;

    /// <summary>
    /// Vida atual do personagem.
    /// </summary>
    public int Health { get; private set; }

    /// <summary>
    /// Se este personagem está morto.
    /// </summary>
    public bool Died => Health == 0;

    /// <summary>
    /// Se o personagem está atordoado.
    /// </summary>
    public bool Stuned { get; private set; }

    /// <summary>
    /// Vida máxima do personagem.
    /// </summary>
    public int MaxHealth => maxHealth;

    /// <summary>
    /// Vida atual do personagem normalizada[0,1].
    /// </summary>
    public float NormalizedHealth => (float)Health / (float)MaxHealth;

    public void Heal(int amount)
    {
        Health += amount;
        Health = Mathf.Min(Health, MaxHealth);
        OnChangeHealth?.Invoke();
    }

    public void Kill(GameObject owner)
    {
        TakeDamage(MaxHealth, owner);
    }

    public void TakeDamage(int damage, GameObject owner)
    {
        if (Died)//se já morreu não faz nada
            return;

        Health -= damage;
        Health = Mathf.Max(Health, 0);

        Stun();
        SpawnDamageEffect();

        OnDamage?.Invoke(new DamageEvent{ Damage = damage, Owner = owner});
        OnChangeHealth?.Invoke();

        if(Died)
        {
            OnDeath?.Invoke();
        }
    }

    private void SpawnDamageEffect()
    {
        Instantiate(damageEffect, transform.position, damageEffect.transform.rotation, transform);
    }

    private void Stun()
    {
        if (Stuned)
            return;

        OnStun?.Invoke();
        StartCoroutine(DoStun());
    }

    IEnumerator DoStun()
    {
        Stuned = true;
        yield return new WaitForSeconds(STUN_DURARION);
        Stuned = false;
    }
}
