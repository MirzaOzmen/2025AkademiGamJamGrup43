using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int destroyTime;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }
}
