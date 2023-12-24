using System;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefub;
    [SerializeField, Range(1, 300)] private int capacity = 50;

    public Action<GameObject> OnInstantiateBullet;

    protected ObjectPool<GameObject> pool;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            CreateItem, 
            (obj) => obj.GetComponent<Destroyable>().InitSelf(), 
            (obj) => obj.GetComponent<Destroyable>().DestroySelf(), 
            (obj) => Destroy(obj), 
            false, capacity, capacity);
    }

    private GameObject CreateItem()
    {
        GameObject item = Instantiate(bulletPrefub);
        OnInstantiateBullet(item);
        return item;
    }

    public void Fire()
    {
        GameObject bullet = pool.Get();
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
