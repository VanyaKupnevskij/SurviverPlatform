using System.Collections;
using UnityEngine;

public class DeadTimer : MonoBehaviour
{
    [SerializeField, Range(0, 20)] public float timeToDead = 7.0f;
    public Destroyable destroyable;

    private void Start()
    {
        destroyable = destroyable ? destroyable : GetComponent<Destroyable>();

        StartCoroutine(Deading());
    }

    private IEnumerator Deading()
    {
        yield return new WaitForSeconds(timeToDead);
        destroyable.DestroySelf();
    }
}
