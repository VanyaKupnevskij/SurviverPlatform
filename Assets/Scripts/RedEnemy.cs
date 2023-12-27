using UnityEngine;

public class RedEnemy : Enemy 
{
    [SerializeField] private float startHealth = 50;

    private void Start()
    {
        Constructor(startHealth);
    }   
}
