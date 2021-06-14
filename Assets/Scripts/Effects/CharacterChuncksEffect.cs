using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChuncksEffect : MonoBehaviour
{
    [SerializeField]
    private float explosionForce = 100f;
    [SerializeField]
    private float explosionRadius = 4f;
    [SerializeField]
    private float upModifier = 20f;
    [SerializeField]
    private float effectDuration = 6f;
    [SerializeField]
    private GameObject bloodEffect;
    [SerializeField]
    private Rigidbody[] chuncks;

    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        foreach(var rb in chuncks)
        {
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
            Destroy(rb.gameObject, effectDuration);
        }

        Destroy(gameObject);
    }
}
