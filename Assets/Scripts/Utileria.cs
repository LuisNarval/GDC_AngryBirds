using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utileria : MonoBehaviour{
    [Header("CONSULTA")]
    public Score code_Score;
    AudioSource SFX;

    // Start is called before the first frame update
    void Start(){
        code_Score = GameObject.Find("ScoreController").GetComponent<Score>();
        SFX = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        float puntos = collision.relativeVelocity.magnitude*10;
        code_Score.aumentarScore(puntos);

        if (collision.relativeVelocity.magnitude > 2.0f)
            SFX.Play();
    }
}