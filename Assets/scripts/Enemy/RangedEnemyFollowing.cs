using UnityEngine;
using UnityEngine.AI;


public class RangedEnemyFollowing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject PlayerTarget;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private float stopDistance = 3f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject FireRotation;
    [SerializeField] private float attackPeriod;
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float sizeOfGun;
    private float timer = 0 ;
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        agent.SetDestination(PlayerTarget.transform.position);
        Vector2 direction = PlayerTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90f));
        FireRotation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Prefab.transform.rotation = Quaternion.identity;
        if (PlayerTarget)
        {
            if (PlayerTarget.transform.position.x > transform.position.x)
            {
                Prefab.transform.localScale = Vector3.one;
                Gun.transform.localScale = new Vector3(sizeOfGun, sizeOfGun, 1);
            }
            else
            {
                Prefab.transform.localScale = new Vector3(-1, 1,1);
                Gun.transform.localScale = new Vector3(-sizeOfGun, -sizeOfGun, 1);
                
            }
        }
        float distance = Vector2.Distance(transform.position, PlayerTarget.transform.position);
        if (distance > stopDistance)
        {
            Anim.SetBool("Walk", true);
            agent.isStopped = false;
            agent.SetDestination(PlayerTarget.transform.position);
        }
        else
        {
            Anim.SetBool("Walk", false);
            timer += Time.deltaTime;
            if(timer>attackPeriod)
            {
                timer = 0;
                Transform.Instantiate(bullet, Gun.transform.position, Gun.transform.rotation);
            }
            if (agent.isStopped)
            {
                agent.SetDestination(PlayerTarget.transform.position);  
            }
            agent.isStopped = true;
            
        }

     

    }
    
}
