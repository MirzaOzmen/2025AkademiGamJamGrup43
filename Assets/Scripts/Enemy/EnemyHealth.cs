using UnityEngine;

public class EnemyHealth : MonoBehaviour , Idamegeable
{
    [SerializeField] private int health;

    [SerializeField] private Teams team;
    public Teams Team => team;

    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}
