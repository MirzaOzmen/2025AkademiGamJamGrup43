using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int DamageAmount;

   
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Idamegeable damagebleparent = collision.transform.GetComponentInParent<Idamegeable>();
        if (collision.TryGetComponent<Idamegeable>(out Idamegeable IDamageable)&&collision.gameObject.tag=="Enemy")
        {
           
                IDamageable.ChangeHealthOfTheCharacter(DamageAmount * -1);
            if (collision.TryGetComponent<knockback>(out knockback enemy))
            {
                enemy.ApplyKnockback(transform.position, 2f); 
            }

        }
        else if (damagebleparent != null && collision.gameObject.tag == "Enemy")
        { 
            IDamageable.ChangeHealthOfTheCharacter(DamageAmount * -1);
            if (collision.TryGetComponent<knockback>(out knockback enemy))
            {
                enemy.ApplyKnockback(transform.position, 2f); 
            }


        }
        else if (damagebleparent != null && collision.gameObject.tag == "Player")
        {
            IDamageable.ChangeHealthOfTheCharacter(DamageAmount * -1);
          /*  if (collision.TryGetComponent<knockback>(out knockback enemy))
            {
                enemy.ApplyKnockback(transform.position, 2f); // 2f = itme gücü
            }*/


        }
        flasheffect flashEffect = collision.transform.GetComponentInParent<flasheffect>();
        if (collision.gameObject.TryGetComponent<flasheffect>(out flasheffect flash_Effect))
        {
            flash_Effect.flashEffect();
        }
        else if ((flashEffect != null && collision.gameObject.tag == "Enemy"))
        {
            Debug.Log("colidlandi");
            flashEffect.flashEffect();
        }


    }
}
