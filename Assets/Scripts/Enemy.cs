using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; } = new Health();

    public Destroyable destroyable;
}
