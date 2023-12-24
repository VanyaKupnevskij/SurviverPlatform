using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Health Health { get; set; } = new Health();
    public Energy Energy { get; set; } = new Energy();

    public PlayerMove playerMove;
    public DestroyablePlayer playerDestroyable;

    private void Start()
    {
        playerMove = playerMove ?? FindObjectOfType<PlayerMove>();
        playerDestroyable = playerDestroyable ?? GetComponent<DestroyablePlayer>();

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
