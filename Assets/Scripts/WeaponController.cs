using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    bool m_isAttacking = false;
     bool m_isCollecting = false;
    public Text charges;

    public int m_charge = 0;
    void Start()
    {
        charges.text = "Charges : 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            GetComponent<Animation>().Play();
            m_isCollecting=false;
            m_isAttacking=true;
        }
        else if(Input.GetMouseButtonDown(1)) {
            GetComponent<Animation>().Play();
            m_isAttacking=false;
            m_isCollecting=true;
        }

        if(!GetComponent<Animation>().isPlaying)
        {
            m_isAttacking=false;
            m_isCollecting=false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(m_isCollecting && collision.gameObject.CompareTag("Note")) {
            m_charge++;
            charges.text = "Charges : " + m_charge;
            Destroy(collision.gameObject);
        }
        if(m_isAttacking && collision.gameObject.CompareTag("Note")) {
            collision.GetComponent<Rigidbody2D>().velocity*=-1;
            collision.GetComponent<Note>().m_reflected = true;
        }
        //if(collision.gameObject.CompareTag("Enemy")) {
        //    collision.GetComponent<EnemyHealth>().TakeDamage(1);
        //}
    }
}
