using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class playercontrolSoldier : MonoBehaviour
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

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    private float timer = 0;
    private bool canAttack = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (isDashing) return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)transform.position;


        float angleZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0f, 0f, angleZ );

        if (!isAttacking)
        {
            gun.localPosition = lookDir.normalized * 0.5f;
        }


        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            // StartCoroutine(SpearAttack());
        }
        if (mousePos.x > transform.position.x)
        {


            transform.localScale = Vector3.one;

            firingPoint.localScale = new Vector3(1, 1, 1);
            firingPoint.localRotation = Quaternion.Euler(0, 0, -90f);
            gun.transform.localScale = new Vector3(x, y, z);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gun.transform.localScale = new Vector3(-x, -y, z);

            firingPoint.localScale = new Vector3(1, -1, 1);
            firingPoint.localRotation = Quaternion.Euler(0, 0, 90f);


        }
        if (transform.localScale.x > 0)
        {
            gun.rotation = Quaternion.Euler(0f, 0f, angleZ );
            if (!isAttacking)
                gun.localPosition = lookDir.normalized * 0.5f;
        }
        else
        {

            gun.rotation = Quaternion.Euler(0f, 0f, angleZ );
            if (!isAttacking)
                gun.localPosition = new Vector3(-lookDir.normalized.x, lookDir.normalized.y, 0) * 0.5f;
        }


        if (Input.GetMouseButton(0)) // basýlý tutuluyorsa
        {
            timer += Time.deltaTime;

            if (timer >= attackPeriod)
            {
                timer = 0f;
                StartCoroutine(spawnbullet());
            }
        }
        else
        {
            // buton býrakýlýrsa zamanlayýcýyý sýfýrla (isteðe baðlý)
            timer = attackPeriod; // istersen burada 0f da yapabilirsin
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
    public IEnumerator spawnbullet()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firingPoint.position).normalized;
            GameObject newBullet = Instantiate(bullet, firingPoint.position, Quaternion.identity);
            newBullet.GetComponent<PlayerBullet>().SetDirection(mouseDir);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            StartCoroutine(sceneChange());
        }
    }
    private IEnumerator sceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
