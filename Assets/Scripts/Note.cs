using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour
{
    public int m_damage=1;
    public Vector2 m_dir;
    public float m_speed = 10f;
    private SpriteRenderer m_SpriteRenderer;

    public Color[] m_colors;
    public bool m_reflected=false;
    
    public GameObject m_doubleNote;
    public bool m_combining=true;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if(m_combining)
            m_SpriteRenderer.color=m_colors[Random.Range(0, m_colors.Length)];
        if(m_dir != null)
            GetComponent<Rigidbody2D>().velocity = m_dir.normalized*m_speed;

        StartCoroutine("Fade");
        StartCoroutine("WaitBeforeCombining");
    }


    IEnumerator Fade() {
        while(m_SpriteRenderer.color.a > 0f) {
            Color coolor = m_SpriteRenderer.color;
            coolor.a -= 0.05f;
            m_SpriteRenderer.color = coolor;
            yield return new WaitForSeconds(0.15f);
        }
        
    }

     IEnumerator WaitBeforeCombining() {
        yield return new WaitForSeconds(0.5f);
        m_combining=false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerHealth>().TakeDamage(m_damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Note") && !m_combining && !collision.GetComponent<Note>().m_combining) {
            m_combining=true;
            print("collision");
            collision.GetComponent<Note>().m_combining=true;
            CombineNotes(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);

        else if(m_reflected && collision.gameObject.CompareTag("Enemy")) {
            collision.GetComponent<EnemyHealth>().TakeDamage(m_damage);
            Destroy(gameObject);
        }
            
        
    }

    private void CombineNotes(GameObject otherNote) {
        if(m_SpriteRenderer.color.Equals(otherNote.GetComponent<SpriteRenderer>().color)) {

            GameObject projectile = Instantiate(m_doubleNote, transform.position, Quaternion.identity);
            projectile.GetComponent<Note>().m_combining=false;
            projectile.GetComponent<SpriteRenderer>().color=m_SpriteRenderer.color;
            print(projectile.name);
            float mAngle = Vector2.Angle(m_dir, otherNote.GetComponent<Note>().m_dir);
             mAngle *= Mathf.Deg2Rad;
            //Vector2 dir = new Vector2(Mathf.Cos(mAngle), Mathf.Sin(mAngle));
            //Vector2 dir = m_dir*otherNote.GetComponent<Note>().m_dir;
            Vector2 dir = Vector2.Min(m_dir, otherNote.GetComponent<Note>().m_dir) * (Vector2.Angle(m_dir, otherNote.GetComponent<Note>().m_dir)/2f);

            projectile.GetComponent<Note>().m_dir = dir;
            Destroy(this.gameObject);
            Destroy(otherNote.gameObject);
        }
    }
}
