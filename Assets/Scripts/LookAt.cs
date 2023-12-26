using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        target = target ? target : FindAnyObjectByType<Camera>().transform;
    }

    void Update()
    {
        transform.forward = target.forward;
    }
}
