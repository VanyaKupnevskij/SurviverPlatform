using UnityEngine;
using UnityEngine.AI;

public enum WhoFollow
{
    Player,
    Other
}

public class MoveTo : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float extraRotationSpeed = 5.0f;
    [SerializeField] private float minDistanceVoid = 0.1f;
    [SerializeField] private bool canFollowVoid = false;

    public Transform Target { get; set; }
    public WhoFollow whoFollow = WhoFollow.Other;
    public Destroyable destroyable;

    private bool isFollowVoid = false;
    private Vector3 TargetPosition => Target.position;

    private void Start()
    {
        agent = agent ?? GetComponent<NavMeshAgent>();
        destroyable = destroyable ?? GetComponent<Destroyable>();
    }

    private void Update()
    {
        agent.destination = new Vector3(TargetPosition.x, agent.destination.y, TargetPosition.z);
        extraRotation();

        if (isFollowVoid && 
            Vector3.SqrMagnitude(transform.position - TargetPosition) < minDistanceVoid * minDistanceVoid)
        {
            destroyable.DestroySelf();
        }
    }

    public void SetFollowVoid(Transform voidTransform)
    {
        if (canFollowVoid)
        {
            Target = voidTransform;
            isFollowVoid = true;
        }
    }

    private void extraRotation()
    {
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
    }
}
