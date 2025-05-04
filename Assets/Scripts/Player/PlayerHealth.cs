using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour , Idamegeable
{
 
    [SerializeField] private int Health;
    [SerializeField] private Teams team;
    [SerializeField] private Slider Slider;
    public Teams Team => team;
    public void Start()
    {
        Slider.maxValue = 100;
        Slider.value = 100;
    }

    public void ChangeHealthOfTheCharacter(int amount)
    {

        Health += amount;
        if (Health >= 100)
        {
            Health = 100;
            Slider.value = Health;
        }
        else
        {
            Health += amount;
            Slider.value = Health;
        }
        if (Health <= 0)
        {
            Health = 0;
            Slider.value = Health;
            //  Destroy(gameObject);
        }
    }
}
