using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; } = new Health();

    public Destroyable destroyable;

    public void Constructor()
    {
        destroyable.OnTakeDamage += Health.TakeDamage;
        destroyable.OnDestorySelf += Health.Dead;
    }
}
