using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerdo : MonoBehaviour{

    [Header("CONFIGURACION")]
    public float Sensibilidad = 1.0f;

    [Header("REFERENCIAS")]
    public GameObject PopUp;
    public AudioClip[] clips;

    [Header("CONSULTA")]
    public int ESTADO = 1;

    private LevelManager code_levelManager;
    private Score code_Score;
    private Animator anim_Cerdo;
    

    


    // Start is called before the first frame update
    void Start(){
        anim_Cerdo = this.GetComponent<Animator>();
        code_levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        code_Score = GameObject.Find("ScoreController").GetComponent<Score>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.relativeVelocity.magnitude > Sensibilidad){
            anim_Cerdo.Play("Explosion");
            PopUp.transform.position = this.transform.position;
            PopUp.GetComponent<Animator>().Play("Oculto");
            Puff();
            PopUp.GetComponent<Animator>().Play("Muestra");
            code_Score.aumentarScore(5000);
            Invoke("DeclararDerrota", 1.0f);
        }
    }

    void DeclararDerrota(){
        ESTADO = 0;
        PopUp.transform.position = Vector3.one * -100;
        this.transform.position = Vector3.one * 10000;
        this.gameObject.SetActive(false);
        code_levelManager.cerdoDestruido();
    }


    void Puff(){
        int aleatorio = (int)Random.Range(0.0f, 2.0f);
        this.gameObject.GetComponent<AudioSource>().clip = clips[aleatorio];
        this.gameObject.GetComponent<AudioSource>().Play();

    }

}
