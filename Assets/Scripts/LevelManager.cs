using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Número de Aves para conseguir 3 estrellas")]
    [Header("----- CONFIGURACIONES -----")]
    public int AvesTresEstrellas;
    [Header("Número de Aves para conseguir 2 estrellas")]
    public int AvesDosEstrellas;

    
    [Header("ACTORES ACTUALES")]
    [Header("----- REFERENCIAS -----")]
    public Cerdo[] cerdos;
    public Ave[] aves;
    public Rigidbody2D[] utileria;

    [Header("Scripts")]
    public Score code_Score;
    public BGMController code_BGM;
    public LevelBreaker code_LevelBreaker;
    public Resortera code_Resortera;
    public CameraController code_Camara;
    public GameObject ColisionesResortera;

    [Header("Animaciones")]
    public Animator anim_Resultados;
    public Animator anim_Estrellas;
    public Animator anim_Derrota;
    public GameObject PopUp_Aves;

    [Header("Audio")]
    public AudioSource SFX_RisaPajaros;
    public AudioSource SFX_RisaCerdos;
    public AudioSource SFX_Mil;
    public AudioClip[] ClipMil;
    public AudioSource SFX_Estrellas;
    public AudioClip[] ClipEstrellas;
    public AudioSource SFX_Fanfarrea;
    public AudioClip[] ClipFanfarrea;

    private bool Victoria = false;

    [Header("----- CONSULTA -----")]
    public int puntosConseguidos = 0;

    private void Start(){
        code_BGM = GameObject.Find("BGM").GetComponent<BGMController>();
    }

    public void cerdoDestruido() {
        bool cerdosEliminados = true;
        for (int i = 0; i < cerdos.Length; i++) {
            if (cerdos[i].ESTADO == 1) {
                cerdosEliminados = false;
                break;
            }
        }
        if (cerdosEliminados)
            TodosLosCerdosFueronEliminados();
    }

    void TodosLosCerdosFueronEliminados() {
        Victoria = true;
        code_Resortera.StopAllCoroutines();
        code_Resortera.enabled = false;
        ColisionesResortera.SetActive(false);
        StartCoroutine(conteoFinalDePuntos());
    }

    
    IEnumerator conteoFinalDePuntos() {

        code_Camara.Victoria();
        code_LevelBreaker.AbrirSiguienteNivel();

        bool movimiento = true;

        do{
            movimiento = false;
            for (int i = 0; i < aves.Length; i++){
                if (aves[i].gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f) {
                    movimiento = true;
                }
            }
            Debug.Log("Esperando Aves");
            yield return new WaitForSeconds(0.1f);
        } while (movimiento);

        do{
            movimiento = false;
            for (int i = 0; i < utileria.Length; i++){
                if (utileria[i].velocity.magnitude > 0.1f){
                    movimiento = true;
                }
            }
            Debug.Log("Esperando Utileria");
            yield return new WaitForSeconds(0.1f);
        } while (movimiento);



        for (int i = 0; i < aves.Length; i++){
            if (aves[i].lanzado == false){
                PopUp_Aves.transform.position = aves[i].transform.position;
                
                PopUp_Aves.GetComponent<Animator>().Play("Oculto");
                PopUp_Aves.GetComponent<Animator>().Play("Muestra");
                yield return new WaitForSeconds(0.25f);
                code_Score.aumentarScore(10000);
                SFXPopUp();
                yield return new WaitForSeconds(1.25f);
                puntosConseguidos++;
                Debug.Log("PuntosSumados");
            }
        }

        PopUp_Aves.transform.position = Vector3.one*-100;
        PopUp_Aves.GetComponent<Animator>().Play("Oculto");

        do{
            movimiento = false;
            for (int i = 0; i < utileria.Length; i++){
                if (utileria[i].velocity.magnitude > 0.02f) {
                    movimiento = true;
                }
            }
            Debug.Log("Esperando Utileria");
            yield return new WaitForSeconds(0.1f);
        } while (movimiento);



        Debug.Log("SE HA GANADO EL JUEGO, SE TERMINO EL CONTEO");
        code_Score.Recibiendo = false;
        code_Score.guardarHighScore();
        anim_Resultados.Play("Resultados_Victoria");
        code_BGM.bajarVolumen();
        SFXFanfarreas(1);

        yield return new WaitForSeconds(1.0f);

        

        StartCoroutine(corrutina_Estrellas());
    }




    IEnumerator corrutina_Estrellas(){

        

        yield return new WaitForSeconds(1.0f);
        anim_Estrellas.Play("Estrellas_Aparecer");
        yield return new WaitForSeconds((1.0f/60.0f)*45.0f);
        anim_Estrellas.Play("Estrellas_Tres");

        aves[0].gameObject.SetActive(false);
        SFX_RisaPajaros.Play();
        
        if (puntosConseguidos >= AvesTresEstrellas){
            SFXEstrellas(3);
            yield return new WaitForSeconds(1.5f);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("3 ESTRELLAS");
        }   
        else if (puntosConseguidos >= AvesDosEstrellas){
            SFXEstrellas(2);
            yield return new WaitForSeconds(1.0f);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("2 ESTRELLAS");
        }
        else{
            yield return new WaitForSeconds(0.5f);
            SFXEstrellas(1);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("1 ESTRELLA");
        }
    }

    

    public void GameOver(){
        code_Resortera.enabled = false;
        ColisionesResortera.SetActive(false);

        for(int i = 0; i < cerdos.Length; i++)
            cerdos[i].enabled = false;

        if (!Victoria){
            anim_Derrota.Play("Derrota_Entrada");

            code_BGM.bajarVolumen();
            SFXFanfarreas(0);
            Invoke("RisaCerdo", 2.0f);

            Debug.Log("GAME OVER !!");
        }
    }


    private void RisaCerdo(){
        SFX_RisaCerdos.Play();
    }

    private void SFXPopUp(){
        int aleatorio = (int)Random.Range(0.0f, 3.0f);
        SFX_Mil.clip = ClipMil[aleatorio];
        SFX_Mil.Play();
    }

    private void SFXEstrellas(int i){
        StartCoroutine(corrutina_SFXEstrellas(i));
    }

    IEnumerator corrutina_SFXEstrellas(int E){

        for(int i = 0; i < E; i++){
            SFX_Estrellas.clip = ClipEstrellas[i];
            SFX_Estrellas.Play();
            yield return new WaitForSeconds(0.5f);
        }

    }

    private void SFXFanfarreas(int i){
        SFX_Fanfarrea.clip = ClipFanfarrea[i];
        SFX_Fanfarrea.Play();
    }
}