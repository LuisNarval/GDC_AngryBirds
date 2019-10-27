using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour{

    [Header("CONFIGURACIONES")]
    public string NombreNivel;

    [Header("REFERENCIAS")]
    public Text txt_Score;
    public Text txt_HighScore;
    public Text txt_InfoResultados;
    public Text txt_ScoreResultados;

    [Header("CONSULTAS")]
    public float score;
    public int highScore;
    public bool Recibiendo = false;

    // Start is called before the first frame update
    void Start(){
        if (PlayerPrefs.HasKey(NombreNivel)){
            highScore = PlayerPrefs.GetInt(NombreNivel);
        }else{
            highScore = 0;
        }

        txt_HighScore.text = highScore.ToString();
        score = 0;
        actualizarScores();
        Invoke("comenzar", 1.0f);
    }

    void comenzar(){
        Recibiendo = true;
    }

    public void guardarHighScore(){

        int scoreFinal = (int)score;
        Debug.Log("Score Final : " + scoreFinal);

        if (scoreFinal > highScore){
            PlayerPrefs.SetInt(NombreNivel, scoreFinal);
            txt_InfoResultados.text = "¡ Nueva Puntuación Máxima !";
        }
        else
            txt_InfoResultados.text = "Puntuación Final";

        txt_ScoreResultados.text = scoreFinal.ToString();
    }



    public void aumentarScore(float cantidad){
        if (Recibiendo){
            score += cantidad;
            actualizarScores();
        }
    }

    void actualizarScores(){
        int scoreRedondeado = (int)score;
        txt_Score.text = scoreRedondeado.ToString();
    }


    

}