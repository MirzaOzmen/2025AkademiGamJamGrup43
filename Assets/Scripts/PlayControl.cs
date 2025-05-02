using UnityEngine;

public class PlayControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float dashForce = 20f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool canDash = true;
    private bool isDashing = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //caching
    }

    void Update()
    {
        if (isDashing) return;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Mouse yonune dondurmek icin
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }


    System.Collections.IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = Vector2.zero;


        rb.AddForce(moveInput * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f); // Dash süresi
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - 0.1f); // Kalan cooldown
        canDash = true;
    }


}
