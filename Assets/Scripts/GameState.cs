using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Singleton
    public static GameState Instance{ get; private set; }
    private void Awake()
    {
        Instance = this;
        GamePaused = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
    }

    /// <summary>
    /// Se o jogo está pausado.
    /// </summary>
    public bool GamePaused { get; set; }
}
