using UnityEngine;

public class RedEnemy : Enemy 
{
    [SerializeField] private ParticleSystem particleDamage;
    [SerializeField] private float startHealth = 50; 

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
