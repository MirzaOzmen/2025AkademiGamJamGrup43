using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        

        agent.SetDestination(PlayerTarget.transform.position);
    Vector2 direction = PlayerTarget.transform.position - transform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90f;
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
    }
}


