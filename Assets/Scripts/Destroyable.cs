using System;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public Action<float> OnTakeDamage;

    public void InitSelf()
    {
        gameObject.SetActive(true);
    }
    
    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage(damage);
    }
}
