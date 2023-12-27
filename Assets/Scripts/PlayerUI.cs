using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderEnergy;
    [SerializeField] private GameObject textUlta;
    [SerializeField] private Button buttonUlta;
    [SerializeField] private ParticleSystem particleDamage;

    public Player player;

    private float Health => player.Health.Value;
    private float Energy => player.Energy.Value;
    private float MaxEnergy => player.Energy.maxValue;

    private void Start()
    {
        player = player ? player : FindObjectOfType<Player>();

        player.Health.OnChange += HandleChangeHP;
        player.Health.OnDead += HandleDead;
        player.Energy.OnChange += HandleChangeEnergy;
        player.Energy.OnAccamulateUlta += HandleAccamulateUlta;
    }

    private void HandleChangeHP(float newValue)
    {
        if (sliderHP.value > newValue)
        {
            particleDamage?.Play();
        }
        sliderHP.value = newValue;
    }

    private void HandleChangeEnergy(float newValue)
    {
        sliderEnergy.value = newValue;

        if (Energy < MaxEnergy)
        {
            textUlta.SetActive(false);
            buttonUlta.interactable = false;
        }
    }

    private void HandleAccamulateUlta()
    {
        textUlta.SetActive(true);
        buttonUlta.interactable = true;
    }
    
    private void HandleDead()
    {
        textUlta.SetActive(false);
    }
}
