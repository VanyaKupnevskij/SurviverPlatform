using UnityEngine;

public enum WhoDamagedType
{
    Player,
    Enemy,
    Wall
}

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage = 15;
    [SerializeField] protected WhoDamagedType whoDamaged = WhoDamagedType.Player;
    [SerializeField] protected Destroyable destroyable;

    private void Start()
    {
        destroyable = destroyable ? destroyable : GetComponent<Destroyable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageTriggered(other);
    }

    protected virtual void DamageTriggered(Collider other)
    {
        if (other.CompareTag(whoDamaged.ToString()))
        {
            other.GetComponent<Destroyable>()?.TakeDamage(damage);
            destroyable.DestroySelf();
        }
    }
}
