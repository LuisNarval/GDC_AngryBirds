using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utileria : MonoBehaviour{
    [Header("CONSULTA")]
    public Score code_Score;

    // Start is called before the first frame update
    void Start(){
        code_Score = GameObject.Find("ScoreController").GetComponent<Score>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        float puntos = collision.relativeVelocity.magnitude*10;
        code_Score.aumentarScore(puntos);
    }
}