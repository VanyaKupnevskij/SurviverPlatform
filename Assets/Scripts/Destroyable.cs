using System;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public Action<float> OnTakeDamage;
    public Action OnDestorySelf;

    public void InitSelf()
    {
        gameObject.SetActive(true);
        OnDestorySelf += () => { };
    }
    
    public void DestroySelf()
    {
        OnDestorySelf();
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage(damage);
    }
}
