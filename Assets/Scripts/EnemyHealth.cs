using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int life=3;
    private int health=10;
    private int defaultHealth=10;
    private int barrier=10;
    private int defaultBarrier=10;

    public Image[] lifes;
    public Slider barrierHealth;
    public Image sliderBackground;
    public Image sliderFill;
    

    void Start() {
        ChangeSlider(true);
    }

    public void TakeDamage(int damage) {
        barrierHealth.value--;
        if(barrier>0) {
            barrier-=damage;
            if(barrier<=0)
                ChangeSlider(false);
        }
        else if(health>0) {
            health-=damage;
        }
        else {
            life--;
            lifes[life].enabled =false;
            if(life<=0)
                Die();
            else {
                barrier=defaultBarrier;
                health=defaultHealth;
                ChangeSlider(true);
            }
            

        }
    }

    public void ChangeSlider(bool barrierSlider){
        if(barrierSlider) {
            sliderFill.color=Color.blue;
            sliderBackground.color=Color.green;
            barrierHealth.value=defaultBarrier;
        }
        else {
            sliderFill.color=Color.green;
            sliderBackground.color=Color.red;
            barrierHealth.value=defaultHealth;
        }
    }

    private void Die() {
        GetComponent<EnemyController>().canShoot=false;
        Destroy(gameObject);
;    }
}
