using UnityEngine;

public class PlayerBulletMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField, Range(0, 10)] private float speed = 1.0f;

    private Vector3 moveDirection = Vector3.forward;

    private void Start()
    {
        rigid = rigid ? rigid : GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigid.transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
