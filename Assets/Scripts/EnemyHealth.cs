using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int m_life=3;
    private int m_health=10;
    private int m_defaultHealth=10;
    private int m_barrier=10;
    private int m_defaultBarrier=10;

    public Image[] lifes;
    public Slider barrierHealth;
    public Image sliderBackground;
    public Image sliderFill;
    

    void Start() {
        ChangeSlider(true);
    }

    public void TakeDamage(int damage) {
        barrierHealth.value--;
        if(m_barrier>0) {
            m_barrier-=damage;
            if(m_barrier<=0)
                ChangeSlider(false);
        }
        else if(m_health>0) {
            m_health-=damage;
        }
        else {
            m_life--;
            lifes[m_life].enabled =false;
            if(m_life<=0)
                Die();
            else {
                m_barrier=m_defaultBarrier;
                m_health=m_defaultHealth;
                ChangeSlider(true);
            }
            

        }
    }

    public void ChangeSlider(bool barrierSlider){
        if(barrierSlider) {
            sliderFill.color=Color.blue;
            sliderBackground.color=Color.green;
            barrierHealth.value=m_defaultBarrier;
        }
        else {
            sliderFill.color=Color.green;
            sliderBackground.color=Color.red;
            barrierHealth.value=m_defaultHealth;
        }
    }

    private void Die() {
        GetComponent<EnemyShootController>().m_canShoot=false;
        Destroy(gameObject);
;    }
}
