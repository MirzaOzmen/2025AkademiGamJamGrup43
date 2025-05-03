using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {

        agent.SetDestination(PlayerTarget.transform.position);
        Vector2 direction = PlayerTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.up * rayDistance, Color.red);
        
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Raycast bir þeye çarptý: " + hit.collider.name);
            agent.isStopped = true;
        }


    }
}


