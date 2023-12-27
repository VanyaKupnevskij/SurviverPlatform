using UnityEngine;

public class RedEnemy : Enemy 
{
    [SerializeField] private float startHealth = 50;
    [SerializeField] private ParticleSystem particleDamage;

    private void Start()
    {
        Constructor(startHealth);

        destroyable.OnTakeDamage += HandleTakeDamage;
    }   

    private void HandleTakeDamage(float damage)
    {
        particleDamage?.Play();
    }
}
