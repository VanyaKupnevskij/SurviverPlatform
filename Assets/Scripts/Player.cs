using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health { get; set; } = new Health();
    public Energy Energy { get; set; } = new Energy(50);

    public PlayerMove playerMove;
    public DestroyablePlayer playerDestroyable;

    private void Start()
    {
        playerMove = playerMove ? playerMove : FindObjectOfType<PlayerMove>();
        playerDestroyable = playerDestroyable ? playerDestroyable : GetComponent<DestroyablePlayer>();

        playerDestroyable.OnTakeDamage += Health.TakeDamage;
        playerDestroyable.OnDecreaseEnergy += Energy.Decrease;
        Health.OnDead += HandleDead;
        Health.OnChange += HandleChangeHP;
    } 

    private void HandleDead()
    {
        playerMove.canControl = false;
    }

    private void HandleChangeHP(float currentHP)
    {
        if (currentHP > 0)
            playerMove.canControl = true;
    }
}
