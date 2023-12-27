using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text textCountKilled;

    private int countKilledEnemy = 0;

    private void Start()
    {
        Resume();

        player = player ? player : FindObjectOfType<Player>();

        player.Health.OnDead += HandleDiePlayer;
        player.spawnerBullet.OnStrike += HandleKilledEnemy;
        player.spawnerBullet.OnRicochet += HandleKilledEnemy;
    }

    private void HandleDiePlayer()
    {
        Pause();
        ShowDiePanel();
    }

    private void HandleKilledEnemy(Type typeEnemy)
    {
        countKilledEnemy++;
        textCountKilled.text = "KILED: " + countKilledEnemy.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void ShowDiePanel()
    {
        menuButton.SetActive(false);
        diePanel.SetActive(true);
    }
}
