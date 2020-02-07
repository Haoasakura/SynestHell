using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health=10;
    public bool invulnerable=false;

    public Text healthText;
    void Start() {
        healthText.text = "Health : "+ health;
    }

    void Update() {
        if(invulnerable){
            GetComponent<SpriteRenderer>().color=Color.green;
        }
        else {
             GetComponent<SpriteRenderer>().color=Color.white;
        }
    }

    public void TakeDamage(int damage) {
        if(!invulnerable) {
            health-=damage;
            healthText.text = "Health : "+ health;
            if(damage<=0)
                Die();
        }
    }

    public void Die() {
        GetComponent<PlayerController>().canMove=false;
    }
}
