using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int DamageAmount;
    
    [SerializeField] private Teams team;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Idamegeable damagebleparent = collision.transform.GetComponentInParent<Idamegeable>();
        if(collision.TryGetComponent<Idamegeable>(out Idamegeable IDamageable))
        {
            if(team != IDamageable.Team)
            {
                IDamageable.ChangeHealthOfTheCharacter(DamageAmount * -1);
                Destroy(gameObject);
            }
        }
        else if (damagebleparent != null)
        {
            if (team != IDamageable.Team)
            {
                IDamageable.ChangeHealthOfTheCharacter(DamageAmount * -1);
                Destroy(gameObject);
            }
        }
        else // wall etc.
        {
            if (collision.tag != gameObject.tag )
                GameObject.Destroy(gameObject);

        }
    }
}
