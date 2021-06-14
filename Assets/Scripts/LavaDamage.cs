using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject fireEffect;

    private void OnTriggerEnter(Collider other)
    {
        var stats = other.gameObject.GetComponent<CharacterStats>();
        stats.Kill(gameObject);
        Instantiate(fireEffect, other.transform.position, Quaternion.identity);
    }
}
