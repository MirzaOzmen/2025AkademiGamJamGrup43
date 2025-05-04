using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyFollowingKnight : MonoBehaviour, knockback
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
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (PlayerTarget)
        {
            // AI karakterin saðýnda ya da solunda olduðunu kontrol et
            if (PlayerTarget.transform.position.x > transform.position.x)
            {
                Prefab.transform.localScale = Vector3.one;
            }
            else
            {
                Prefab.transform.localScale = new Vector3(-1, 1, 1);
            }

            // Player'a doðru rotasyon hesaplama
            Vector2 direction = PlayerTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Prefab.transform.rotation = Quaternion.identity;
            attackPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
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

            // Silahýn yönünü doðru ayarla
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

        // Saldýrý yönünü hesapla
        Vector2 dir = (PlayerTarget.transform.position - transform.position).normalized;

        // Silahýn mevcut pozisyonu
        Vector3 originalLocalPos = gun.transform.localPosition;

        // Dünya koordinatlarýnda saldýrý pozisyonu
        Vector3 attackWorldPos = gun.transform.position + (Vector3)(dir * attackDistance);

        float elapsed = 0f;

        // Saldýrýyý yapma (ilerleme)
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.transform.position = Vector3.Lerp(gun.transform.position, attackWorldPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gun.transform.position = attackWorldPos;

        elapsed = 0f;

        // Saldýrý sonrasý geri dönüþ
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.transform.position = Vector3.Lerp(attackWorldPos, gun.transform.position, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Silahý orijinal pozisyona geri döndür
        gun.transform.localPosition = originalLocalPos;

        yield return new WaitForSeconds(0.5f); // kýsa bir bekleme süresi

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

            float knockTime = 0.1f; // Knockback süresi
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
