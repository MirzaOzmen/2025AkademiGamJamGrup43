using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyFollowing : MonoBehaviour,knockback
{
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float dashSpeed = 0.001f;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemyHealth health;
    private bool canDashAttack = false;
    private Vector3 dashDirection;
    private bool newmeshbool = true;
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }


    void Update()
    {
        Debug.Log("newmesh = " + newmeshbool);
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
       
        if(newmeshbool)
        {
           
            
            agent.SetDestination(PlayerTarget.transform.position);
            Vector2 direction = PlayerTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, playerLayer);
            Debug.DrawRay(transform.position, transform.up * rayDistance, Color.red);
            Prefab.transform.rotation = Quaternion.identity;
            Debug.Log("durum = " + agent.isStopped);


            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log("Raycast bir �eye �arpt�: " + hit.collider.name);

                animator.SetBool("Walk", false);
                animator.SetBool("Attack", true);

            }
            else
            {

                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            Vector2 direction = PlayerTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, playerLayer);
            Debug.DrawRay(transform.position, transform.up * rayDistance, Color.red);
            Prefab.transform.rotation = Quaternion.identity;
            Prefab.transform.rotation = Quaternion.identity;
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log("Raycast bir �eye �arpt�: " + hit.collider.name);

                animator.SetBool("Walk", false);
                animator.SetBool("Attack", true);

            }
            else
            {

                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }
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
           
            
            Vector2 dashDir = ((Vector2)(PlayerTarget.transform.position - transform.position)).normalized;
            rb.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);



        }

    }
    public void waitForDash()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("DashFinished");

        agent.isStopped = false;
        agent.Warp(transform.position); 
        newmeshbool = true;
        Debug.Log("newmesh = " + newmeshbool);
        StartCoroutine(DashAttack());
    }
    public void stoped()
    {
        newmeshbool = false;
        agent.Stop(true);
    }
    public void stoepdFalse()
    {
        rb.linearVelocity = Vector2.zero; 

        newmeshbool = true;
        agent.Stop(false);
    }
    public void ApplyKnockback(Vector2 sourcePosition, float knockbackForce)
    {
        Vector2 knockDir = (transform.position - (Vector3)sourcePosition).normalized;
        Vector3 knockback = knockDir * knockbackForce;

        StartCoroutine(KnockbackRoutine(knockback));
    }

    public IEnumerator KnockbackRoutine(Vector3 knockback)
    {
        if (health.health > 0)
        {
            agent.isStopped = true;

            float knockTime = 0.1f; 
            float elapsed = 0f;
            while (elapsed < knockTime)
            {
                transform.position += knockback * Time.deltaTime;
                elapsed += Time.deltaTime;
                yield return null;
            }

            agent.isStopped = false;
        }

    }
}


