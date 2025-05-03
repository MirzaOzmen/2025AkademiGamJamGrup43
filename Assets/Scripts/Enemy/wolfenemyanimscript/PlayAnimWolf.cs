using UnityEngine;

public class PlayAnimWolf : MonoBehaviour
{
    [SerializeField] private EnemyFollowing enemyfollow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void canDash()
    {
        enemyfollow.canDash();
    }
    public void waitForDash()
    {
        enemyfollow.waitForDash();
    }
    public void stoped()
    {
        enemyfollow.stoped();
    }
    public void stopedFalse()
    {
        enemyfollow.stoepdFalse();
    }
}
