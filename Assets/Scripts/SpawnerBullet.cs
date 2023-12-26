using System;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefub;
    [SerializeField, Range(1, 300)] private int capacity = 50; 
    [SerializeField, Range(0.1f, 10.0f)] private float intervalFire = 3.0f;

    public Action<GameObject> OnInstantiateBullet;
    public bool EnabledIntervalFire { 
        get { return _enabledIntervalFire; }
        set {
            _enabledIntervalFire = value;
            _timerFire = 0.0f;
        } 
    }

    protected ObjectPool<GameObject> pool;

    private float _timerFire = 0.0f;
    private bool _enabledIntervalFire = false;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            CreateItem, 
            (obj) => obj.GetComponent<Destroyable>().InitSelf(), 
            (obj) => obj.GetComponent<Destroyable>().DestroySelf(), 
            (obj) => Destroy(obj), 
            false, capacity, capacity);
    }

    private void Update()
    {
        if (EnabledIntervalFire)
            IntervalFire();
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

    public void IntervalFire()
    {
        if (_timerFire >= intervalFire)
        {
            Fire();
            _timerFire = 0.0f;
        }
        else
        {
            _timerFire += Time.deltaTime;
        }
    }
}
