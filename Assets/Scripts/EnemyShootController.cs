using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    public float m_radius = 1f;
    private float m_spawnTimer;
    public float m_maxSpawn = 1f;

    public GameObject note;

    private Vector2 m_spawnPosition;

    
    public bool m_canShoot=true;

    [SerializeField]
    private Angles[] m_angles;

    [SerializeField]
    private float[] m_startAngles;
    private int m_currentAngle = 0;

    void Update()
    {
        m_spawnTimer -= Time.deltaTime;
        if(m_canShoot && m_spawnTimer <= 0) {
            m_spawnTimer = m_maxSpawn;

            for(int i = 0; i < m_angles[m_currentAngle].angles.Length; i++) {
                m_spawnPosition = new Vector2(transform.position.x, transform.position.y) ;
                
                GameObject projectile = Instantiate(note, m_spawnPosition, Quaternion.identity);
                //float mAngle = StartAngles[(currentAngle) % StartAngles.Length] + (i * m_angleinterval);
                float mAngle = m_angles[m_currentAngle].angles[i];
                mAngle *= Mathf.Deg2Rad;
                
                Vector2 dir = new Vector2(Mathf.Cos(mAngle), Mathf.Sin(mAngle));

               
                projectile.GetComponent<Note>().m_dir = dir;
            }
            m_currentAngle++;
            m_currentAngle %= m_angles.Length;

        }        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
        Gizmos.color = Color.yellow;

    }

    [System.Serializable]
    private struct Angles
    {
        public float[] angles;
    }
}
