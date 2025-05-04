using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] spawnPoint;
    [SerializeField] private int spawnnumber;
    [SerializeField] private GameObject enemyprefeab;
    [SerializeField] private float spawnrate;
    private int characterdiedrate;
    [SerializeField] private GameObject portal;
    private bool canSpawn = true;
    void Start()
    {
        characterdiedrate = spawnnumber;
        StartCoroutine(spawn());
       
    }

    
    void Update()
    {
        if(characterdiedrate<=0 && canSpawn)
        {
            canSpawn = false;
            Instantiate(portal, transform.position, Quaternion.identity);
        }
    }
    private IEnumerator spawn()
    {
        int random;
        while (spawnnumber >= 0)
        {
            random = Random.Range(0, spawnPoint.Length);

            Instantiate(enemyprefeab, spawnPoint[random].transform.position, Quaternion.identity);
            spawnnumber--;
            yield return new WaitForSeconds(spawnrate);
        }
    }
    public void reducenumber()
    {
        characterdiedrate--;
    }
}
