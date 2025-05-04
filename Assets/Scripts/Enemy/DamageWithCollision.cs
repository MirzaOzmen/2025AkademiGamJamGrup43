using UnityEngine;

public class DamageWithCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int damage;
    [SerializeField] private Teams team;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Idamegeable damagebleparent = collision.transform.GetComponentInParent<Idamegeable>();
        if (collision.gameObject.TryGetComponent<Idamegeable>(out Idamegeable damageble))
        {
            if (team != damageble.Team)
            {
                if(collision.gameObject.tag=="Enemy")
                {
                   /* Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                   
                    if (rb!=null)
                    {
                        rb.linearVelocity = Vector2.zero;
                    }*/
                 
                }
                Debug.Log("colidlandi");
                damageble.ChangeHealthOfTheCharacter(-1 * damage);
                flasheffect flashEffect = collision.transform.GetComponentInParent<flasheffect>();
                if (collision.gameObject.TryGetComponent<flasheffect>(out flasheffect flash_Effect))
                {
                    flash_Effect.flashEffect();
                }
                else if ((flashEffect != null && collision.gameObject.tag == "Player"))
                {
                    Debug.Log("colidlandi");
                    flashEffect.flashEffect();
                }



            }
        }
        else if((damagebleparent != null && collision.gameObject.tag == "Player"))
        {
            Debug.Log("colidlandi");
            damageble.ChangeHealthOfTheCharacter(-1 * damage);
            flasheffect flashEffect = collision.transform.GetComponentInParent<flasheffect>();
            if (collision.gameObject.TryGetComponent<flasheffect>(out flasheffect flash_Effect))
            {
                flash_Effect.flashEffect();
            }
            else if ((flashEffect != null && collision.gameObject.tag == "Player"))
            {
                Debug.Log("colidlandi");
                flashEffect.flashEffect();
            }
        }
     
    }
}
