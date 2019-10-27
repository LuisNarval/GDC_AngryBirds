using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour{

    [Header("CONFIGURACIONES")]
    public float Sensibilidad = 1.0f;

    [Header("REFERENCIAS")]
    public GameObject Explosion;

    [Header("CONSULTAS")]
    public Rigidbody2D cuerpo;
    public BoxCollider2D colision;
    public SpriteRenderer sprite;

    private void Start(){
        cuerpo = this.GetComponent<Rigidbody2D>();
        colision = this.GetComponent<BoxCollider2D>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.relativeVelocity.magnitude > Sensibilidad) {
            cuerpo.gravityScale = 0.0f;
            colision.enabled = false;
            sprite.enabled = false;
            Explosion.SetActive(true);
            Invoke("Migrar", 3.0f);
        }
    }

    void Migrar(){
        this.transform.position = Vector3.one * 10000;
    }

}