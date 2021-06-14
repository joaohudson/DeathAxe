using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField]
    private int amount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var stats = other.GetComponent<CharacterStats>();
            if (stats.NormalizedHealth == 1f)
                return;

            stats.Heal(amount);
            Destroy(gameObject);
        }
    }
}
