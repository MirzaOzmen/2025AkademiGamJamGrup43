using UnityEngine;

public class PlayerHealth : MonoBehaviour , Idamegeable
{
 
    [SerializeField] private int Health;
    [SerializeField] private Teams team;
    public Teams Team => team;

    public void ChangeHealthOfTheCharacter(int amount)
    {
        Health += amount;
        if(Health<=0)
        {
          //  Destroy(gameObject);
        }
    }
}
