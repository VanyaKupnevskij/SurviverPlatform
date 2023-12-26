using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderEnergy;
    [SerializeField] private GameObject textUlta;
    [SerializeField] private GameObject textDead;

    public Player playerManager;

    private float Health => playerManager.Health.Value;
    private float Energy => playerManager.Energy.Value;
    private float MaxEnergy => playerManager.Energy.maxValue;

    private void Start()
    {
        playerManager = playerManager ? playerManager : FindObjectOfType<Player>();

        playerManager.Health.OnChange += HandleChangeHP;
        playerManager.Health.OnDead += HandleDead;
        playerManager.Energy.OnChange += HandleChangeEnergy;
        playerManager.Energy.OnAccamulateUlta += HandleAccamulateUlta;
    }

    private void HandleChangeHP(float newValue)
    {
        sliderHP.value = newValue;

        if (Health > 0)
        {
            textDead.SetActive(false);
        }
    }

    private void HandleChangeEnergy(float newValue)
    {
        sliderEnergy.value = newValue;

        if (Energy < MaxEnergy)
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
