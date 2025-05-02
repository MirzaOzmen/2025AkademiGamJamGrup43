using UnityEngine;

public class RangedEnemyFollowing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private float stopDistance = 3f;
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, PlayerTarget.transform.position);
        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(PlayerTarget.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }

        agent.SetDestination(PlayerTarget.transform.position);
        Vector2 direction = PlayerTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
