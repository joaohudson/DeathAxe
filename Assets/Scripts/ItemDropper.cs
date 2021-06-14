using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField][Range(0f, 1f)]
    private float dropRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        var stats = GetComponent<CharacterStats>();
        stats.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        bool drop = Random.Range(0f, 1f) < dropRate;

        if (drop)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
