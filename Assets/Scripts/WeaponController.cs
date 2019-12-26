using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    bool isAttacking = false;
     bool isCollecting = false;
    public Text charges;

    public int charge = 0;
    void Start()
    {
        charges.text = "Charges : 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            GetComponent<Animation>().Play();
            isCollecting=false;
            isAttacking=true;
        }
        else if(Input.GetMouseButtonDown(1)) {
            GetComponent<Animation>().Play();
            isAttacking=false;
            isCollecting=true;
        }

        if(!GetComponent<Animation>().isPlaying)
        {
            isAttacking=false;
            isCollecting=false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(isCollecting && collision.gameObject.CompareTag("Note")) {
            charge++;
            charges.text = "Charges : " + charge;
            Destroy(collision.gameObject);
        }
        if(isAttacking && collision.gameObject.CompareTag("Note")) {
            collision.GetComponent<Rigidbody2D>().velocity*=-1;
        }
    }
}
