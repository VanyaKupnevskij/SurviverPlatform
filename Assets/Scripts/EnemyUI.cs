using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;

    public Enemy enemy;

    private float Health => enemy.Health.Value;

    private void Start()
    {
        enemy = enemy ? enemy : GetComponent<Enemy>();

        enemy.Health.OnChange += HandleChangeHP;
    }

    private void HandleChangeHP(float newValue)
    {
        sliderHP.value = newValue;
    }
    
}
