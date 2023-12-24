using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage = 15;
    [SerializeField] protected string whoDamaged = "Player";
    [SerializeField] protected Destroyable destroyable;

    private void Start()
    {
        destroyable = destroyable ?? GetComponent<Destroyable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(whoDamaged))
        {
            DamageTriggered(other);
            destroyable.DestroySelf();
        }
    }

    protected virtual void DamageTriggered(Collider other)
    {
        other.GetComponent<Destroyable>()?.TakeDamage(damage);
    }
}
