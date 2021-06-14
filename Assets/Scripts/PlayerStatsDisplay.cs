using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;

    private CharacterStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        stats.OnChangeHealth += OnChangeHealth;
    }

    private void OnChangeHealth()
    {
        healthImage.fillAmount = stats.NormalizedHealth;
    }
}
