﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum DashState {
        Ready,
        Dashing,
        Cooldown
    }

    public float m_speed = 10f;

    private float m_vertical;
    private float m_horizontal;

    public DashState dashState;
    public float dashTimer;
    public float maxDash = 1f;
    public bool canMove=true;

    public Vector2 savedVelocity;

    private Rigidbody2D m_rigidbody;

    void Start() {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");

        Vector3 difference =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(-difference.x, difference.y) * Mathf.Rad2Deg;
       
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

    }

    private void FixedUpdate() {
        Vector2 velocity = new Vector2(m_horizontal, m_vertical);
        m_rigidbody.velocity = velocity * m_speed * Time.fixedDeltaTime;

        switch(dashState) {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if(isDashKeyDown) {
                    savedVelocity = m_rigidbody.velocity;
                    m_rigidbody.AddForce(velocity.normalized * 50, ForceMode2D.Impulse);
                    dashState = DashState.Dashing;
                    GetComponent<PlayerHealth>().invulnerable=true;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.fixedDeltaTime * 3;
                if(dashTimer >= maxDash) {
                    dashTimer = maxDash;
                    m_rigidbody.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                    GetComponent<PlayerHealth>().invulnerable=false;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.fixedDeltaTime;
                if(dashTimer <= 0) {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }


    }
}
