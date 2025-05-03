using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Rigidbody2D rb;
    private bool canDashAttack = false;
    private Vector3 dashDirection;
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
        Prefab.transform.rotation = Quaternion.identity;

        if (PlayerTarget)
        {
            if (PlayerTarget.transform.position.x > transform.position.x)
            {
               
                Prefab.transform.localScale = new Vector3(-1, 1, 1);

            }
            else
            {
                Prefab.transform.localScale = Vector3.one;


            }
        }
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Raycast bir �eye �arpt�: " + hit.collider.name);
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);

        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
        }


    }
   public IEnumerator DashAttack()
    {
        yield return new WaitForSeconds(1);
        animator.SetTrigger("DashFinished");
    }
    public void canDash()
    {
        canDashAttack = true;
        dashDirection = (PlayerTarget.transform.position - transform.position).normalized;
        if (canDashAttack)
        {
            canDashAttack = false;
            rb.linearVelocity = dashDirection * dashSpeed;  // Rigidbody'yi kullanarak hareket ettir
           

        }
        
    }
    public void waitForDash()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("DashFinished");
        StartCoroutine(DashAttack());
    }

}


