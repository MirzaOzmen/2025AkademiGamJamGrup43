using UnityEngine;

public class PlayControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float dashForce = 10f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Hareket giriþi
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Mouse yönüne dön
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    System.Collections.IEnumerator Dash()
    {
        canDash = false;
        rb.AddForce(moveInput * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
