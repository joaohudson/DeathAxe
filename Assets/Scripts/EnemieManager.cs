using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave
{
    public int enemiesCount;
    public GameObject enemiePrefab;
}

public class EnemieManager : MonoBehaviour
{
    #region Singleton
    public static EnemieManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private const float WAVE_DELAY = 3f;

    /// <summary>
    /// Chamado quando todas as waves forem finalizadas.
    /// </summary>
    public event Action OnFinish;

    /// <summary>
    /// Chamado quando todos os inimigos forem mortos.
    /// </summary>
    public event Action OnClearEnemies;

    [SerializeField]
    private GameObject spawnEffect;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Wave[] waves;

    private int enemiesCount = 0;
    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(WAVE_DELAY);

        var stay = new WaitForSeconds(.5f);
        Wave wave = waves[currentWave];

        for(int i = 0; i < wave.enemiesCount; i++)
        {
            yield return stay;
            var point = spawnPoints[i];
            Instantiate(spawnEffect, point.position, Quaternion.identity);
            Instantiate(wave.enemiePrefab, point.position, Quaternion.identity);
        }

        enemiesCount = wave.enemiesCount;
        currentWave++;
    }

    /// <summary>
    /// Deve ser chamado por um inimigo para
    /// registrar sua morte.
    /// </summary>
    public void RegisterDeath()
    {
        if (enemiesCount == 0)
            return;

        enemiesCount--;
        if(enemiesCount == 0)
        {
            if(OnClearEnemies != null)
                OnClearEnemies();

            if (currentWave < waves.Length)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                if(OnFinish != null)
                    OnFinish();
            }
        }
    }
}
