using UnityEngine;

public class healthregen : MonoBehaviour
{
    private GameObject hp;
    private PlayerHealth health ;
    void Start()
    {
         hp = GameObject.FindGameObjectWithTag("Player");
         health = hp.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            health.ChangeHealthOfTheCharacter(10);
            Destroy(gameObject);
        }
    }
}
