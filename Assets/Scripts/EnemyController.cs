using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float radius = 1f;
    public float spawnTimer;
    public float maxSpawn = 1f;
    public int spawnAtTime = 3;

    public GameObject note;

    Vector2 spawnPosition;
    float m_angleinterval;
    
    public bool canShoot=true;
    void Start()
    {
        m_angleinterval = 360f / spawnAtTime;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(canShoot && spawnTimer <= 0) {
            spawnTimer = maxSpawn;
            float angle = Random.Range(0f, 360f);
            for(int i = 0; i < spawnAtTime; i++) {
                spawnPosition = new Vector2(transform.position.x, transform.position.y) ;
                
                GameObject projectile = Instantiate(note, spawnPosition, Quaternion.identity);
                float mAngle = angle + (i * m_angleinterval);
                mAngle *= Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(mAngle), Mathf.Sin(mAngle));

               
                projectile.GetComponent<Note>().dir = dir;
            }

        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.yellow;

    }
}
