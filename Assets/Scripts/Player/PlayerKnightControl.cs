using UnityEngine;
using System.Collections;

public class PlayerKnightControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float dashForce = 20f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool canDash = true;
    private bool isDashing = false;

    [SerializeField] private Transform gun;
    [SerializeField] private Animator anim;

    private bool isAttacking = false;
    [SerializeField] private float attackDuration = 0.01f;
    [SerializeField] private float attackDistance = 1.2f;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float attackPeriod;

    [SerializeField] private Transform firingPoint;
    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing) return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        anim.SetBool("walk", moveInput != Vector2.zero);
        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(Dash());
        }

        // Mouse konumuna bak
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angleZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Karakteri mouse yönüne çevir (scale ile)
        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gun.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gun.transform.localScale = new Vector3(1, -1, 1);
        }

        
        if (transform.localScale.x > 0)
        {
            gun.rotation = Quaternion.Euler(0f, 0f, angleZ - 125f);
            if (!isAttacking)
                gun.localPosition = lookDir.normalized * 0.5f;
        }
        else
        {
            
            gun.rotation = Quaternion.Euler(0f, 0f, angleZ + 125f);
            if (!isAttacking)
                gun.localPosition = new Vector3(-lookDir.normalized.x, lookDir.normalized.y, 0) * 0.5f;
        }

       
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SpearAttack());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(moveInput * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - 0.1f);
        canDash = true;
    }

    private IEnumerator SpearAttack()
    {
        isAttacking = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        // Aynalama
        if (transform.localScale.x < 0)
        {
            dir.x *= -1;
        }

        Vector3 originalPos = dir * 0.5f;
        Vector3 attackPos = dir * attackDistance;

        float elapsed = 0f;
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.localPosition = Vector3.Lerp(originalPos, attackPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gun.localPosition = attackPos;

        elapsed = 0f;
        while (elapsed < attackDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / attackDuration);
            gun.localPosition = Vector3.Lerp(attackPos, originalPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gun.localPosition = originalPos;
        isAttacking = false;
    }
}
