using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private Text endText;
    [SerializeField]
    private string loseMessage;
    [SerializeField]
    private string winMessage;

    private void Start()
    {
        var stats = PlayerController.Instance.GetComponent<CharacterStats>();
        stats.OnDeath += OnDeath;
        var enemieManager = EnemieManager.Instance;
        enemieManager.OnFinish += OnFinish;
    }

    private void OnFinish() 
    {
        StartCoroutine(ShowResult(true));
    }

    private void OnDeath()
    {
        StartCoroutine(ShowResult(false));
    }

    IEnumerator ShowResult(bool win)
    {
        GameState.Instance.GamePaused = true;
        yield return new WaitForSeconds(win ? 2f : 3f);
        endText.text = win ? winMessage : loseMessage;
        endMenu.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
