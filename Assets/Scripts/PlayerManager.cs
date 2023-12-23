using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxEnergy = 100;
    public float Health { get; private set; }
    public float Energy { get; private set; }

    public PlayerMove playerMove;
    public Destroyable playerDestroyable;

    public Action OnDead;
    public Action<float> OnChangeHP;
    public Action<float> OnChangeEnergy;
    public Action OnAccamulateUlta;

    private void Start()
    {
        playerMove = playerMove ?? FindObjectOfType<PlayerMove>();

        Health = 100;
        Energy = 50;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamageHP();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IncreaseHP();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            IncreaseEnergy();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DecreaseEnergy();
        }
    }

    public void TakeDamageHP(float damage = 15)
    {
        Health -= damage;
        OnChangeHP(Health);

        if (Health <= 0)
        {
            Health = 0;
            OnDead();
        }
    }

    public void IncreaseHP(float value = 50)
    {
        Health += value;
        OnChangeHP(Health);

        if (Health > maxHealth)
            Health = maxHealth;
    }

    public void IncreaseEnergy(float value = 10)
    {
        Energy += value;
        OnChangeEnergy(Energy);

        if (Energy >= maxEnergy)
        {
            Energy = maxEnergy;
            OnAccamulateUlta();
        }
    }

    public void DecreaseEnergy(float value = 10)
    {
        Energy -= value;
        OnChangeEnergy(Energy);

        if (Energy < 0)
            Energy = 0;
    }
}
