using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    bool isAttacking = false;
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
        }
        isAttacking = GetComponent<Animation>().isPlaying;


    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(isAttacking && collision.gameObject.CompareTag("Note")) {
            charge++;
            charges.text = "Charges : " + charge;
            Destroy(collision.gameObject);
        }
    }
}
