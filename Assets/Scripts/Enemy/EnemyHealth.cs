using UnityEngine;

public class EnemyHealth : MonoBehaviour , Idamegeable
{
    public int health;
    private EnemySpawner spawner;
    [SerializeField] private Teams team;
    public Teams Team => team;

    void Start()
    {

        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeHealthOfTheCharacter(int amount)
    {
        health += amount;
        Debug.Log("damage = " + health);
        if(health<=0)
        {
            spawner.reducenumber();
            Destroy(gameObject);
        }
    }
}
