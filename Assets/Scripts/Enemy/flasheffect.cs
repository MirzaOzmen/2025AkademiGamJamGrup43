using UnityEngine;
using System.Collections;

public class flasheffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CharacterParts;
    [SerializeField] private Color color;

    [SerializeField] private float DamageFlashTime;
    private Color defaultColor;
    void Start()
    {
       
      
            defaultColor = CharacterParts.color;
        

    }


    void Update()
    {

    }
    public void flashEffect()
    {
        StartCoroutine(GotDamage());
    }
    public IEnumerator GotDamage()
    {
        
            CharacterParts.color = color;
        
        yield return new WaitForSeconds(DamageFlashTime);

      
            CharacterParts.color = defaultColor;
        
    }
}
