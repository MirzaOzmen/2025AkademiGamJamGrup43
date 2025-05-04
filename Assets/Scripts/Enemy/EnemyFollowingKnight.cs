
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyFollowingKnight : MonoBehaviour,knockback
{
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private float degree;

    private bool isAttacking = false;
    private bool navmeshEnabled = true;

    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (PlayerTarget)
        {
            if (PlayerTarget.transform.position.x > transform.position.x)
            {

             
                Prefab.transform.localScale = Vector3.one;

            }
            else
            {
                Prefab.transform.localScale = new Vector3(-1, 1, 1);


            }
            Vector2 direction = PlayerTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Prefab.transform.rotation = Quaternion.identity;
            attackPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90f));
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 125f));
            RaycastHit2D hit = Physics2D.Raycast(attackPoint.transform.position, attackPoint.transform.up, rayDistance, playerLayer);
            Debug.DrawRay(attackPoint.transform.position, attackPoint.transform.up * rayDistance, Color.red);
              
        if (navmeshEnabled && PlayerTarget)
        {
            agent.SetDestination(PlayerTarget.transform.position);
        }


        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            animator.SetBool("Walk", false);

            Debug.Log("hitttttttttttttt");
            if (!isAttacking)
                StartCoroutine(EnemySpearAttack());
        }
        else
        {
            if (!isAttacking)
            {
              
                animator.SetBool("Walk", true);
            }
        }
            
            if (PlayerTarget.transform.position.x > transform.position.x)
                gun.transform.localScale = new Vector3(-1, 1, 1);
            else
                gun.transform.localScale = Vector3.one;
        }

      
    }

    private IEnumerator EnemySpearAttack()
    {
        isAttacking = true;
        navmeshEnabled = false;
        agent.isStopped = true;

        Vector2 dir = (PlayerTarget.transform.position - transform.position).normalized;
     
        Vector3 originalPos = dir * 0.5f;
        Vector3 attackPos = dir * attackDistance;

        float elapsed = 0f;
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.transform.localPosition = Vector3.Lerp(originalPos, attackPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gun.transform.localPosition = attackPos;

        elapsed = 0f;
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.transform.localPosition = Vector3.Lerp(attackPos, originalPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gun.transform.localPosition = originalPos;

        yield return new WaitForSeconds(0.5f); 
        navmeshEnabled = true;
        agent.isStopped = false;
        isAttacking = false;
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
