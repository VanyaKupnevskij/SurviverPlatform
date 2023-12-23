using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderEnergy;
    [SerializeField] private GameObject textUlta;
    [SerializeField] private GameObject textDead;

    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = playerManager ?? FindObjectOfType<PlayerManager>();

        playerManager.OnChangeHP += HandleChangeHP;
        playerManager.OnChangeEnergy += HandleChangeEnergy;
        playerManager.OnAccamulateUlta += HandleAccamulateUlta;
        playerManager.OnDead += HandleDead;
    }

    private void HandleChangeHP(float newValue)
    {
        sliderHP.value = newValue;

        if (playerManager.Health > 0)
        {
            textDead.SetActive(false);
        }
    }

    private void HandleChangeEnergy(float newValue)
    {
        sliderEnergy.value = newValue;

        if (playerManager.Energy < playerManager.maxEnergy)
        {
            textUlta.SetActive(false);
        }
    }

    private void HandleAccamulateUlta()
    {
        textUlta.SetActive(true);
    }
    
    private void HandleDead()
    {
        textUlta.SetActive(false);
        textDead.SetActive(true);  
    }
}
