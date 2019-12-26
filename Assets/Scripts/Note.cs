using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour
{
    public int damage=1;
    public Vector2 dir;
    public float speed = 10f;
    private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if(dir != null)
            GetComponent<Rigidbody2D>().velocity = dir.normalized*speed;

        StartCoroutine("Fade");
    }


    IEnumerator Fade() {
        while(m_SpriteRenderer.color.a > 0f) {
            Color coolor = m_SpriteRenderer.color;
            coolor.a -= 0.05f;
            m_SpriteRenderer.color = coolor;
            yield return new WaitForSeconds(0.15f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
