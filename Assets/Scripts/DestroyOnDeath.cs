using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class DestroyOnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterStats>().OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
