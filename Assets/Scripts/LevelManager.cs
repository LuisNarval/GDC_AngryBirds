using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Número de Aves para conseguir 3 estrellas")]
    [Header("CONFIGURACIONES")]
    public int AvesTresEstrellas;
    [Header("Número de Aves para conseguir 2 estrellas")]
    public int AvesDosEstrellas;

    [Header("REFERENCIAS")]
    public Cerdo[] cerdos;
    public Ave[] aves;
    public Rigidbody[] utileria;

    public Score code_Score;
    public Resortera code_Resortera;
    public GameObject ColisionesResortera;

    public Animator anim_Resultados;
    public Animator anim_Estrellas;
    public Animator anim_Derrota;

    public GameObject PopUp_Aves;

    [Header("CONSULTA")]
    public int puntosConseguidos = 0;
    

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
        code_Resortera.enabled = false;
        ColisionesResortera.SetActive(false);
        StartCoroutine(conteoFinalDePuntos());
    }

    
    IEnumerator conteoFinalDePuntos() {

        bool movimiento = true;

        do{
            movimiento = false;
            for (int i = 0; i < utileria.Length; i++){
                if (aves[i].gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.02f) {
                    movimiento = true;
                }
            }
            Debug.Log("Esperando Aves");
            yield return new WaitForSeconds(0.5f);
        } while (movimiento);

        do{
            movimiento = false;
            for (int i = 0; i < utileria.Length; i++){
                if (utileria[i].velocity.magnitude > 0.02f){
                    movimiento = true;
                }
            }
            Debug.Log("Esperando Utileria");
            yield return new WaitForSeconds(0.5f);
        } while (movimiento);



        for (int i = 0; i < aves.Length; i++){
            if (aves[i].lanzado == false){
                PopUp_Aves.transform.position = aves[i].transform.position;
                
                PopUp_Aves.GetComponent<Animator>().Play("Oculto");
                PopUp_Aves.GetComponent<Animator>().Play("Muestra");
                yield return new WaitForSeconds(0.25f);
                code_Score.aumentarScore(10000);
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
            yield return new WaitForSeconds(0.5f);
        } while (movimiento);



        Debug.Log("SE HA GANADO EL JUEGO, SE TERMINO EL CONTEO");

        yield return new WaitForSeconds(1.0f);

        code_Score.guardarHighScore();
        anim_Resultados.Play("Resultados_Victoria");

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(corrutina_Estrellas());
    }




    IEnumerator corrutina_Estrellas(){
        yield return new WaitForSeconds(1.0f);
        anim_Estrellas.Play("Estrellas_Aparecer");
        yield return new WaitForSeconds((1.0f/60.0f)*45.0f);
        anim_Estrellas.Play("Estrellas_Tres");

        aves[0].gameObject.SetActive(false);

        if (puntosConseguidos >= AvesTresEstrellas){   
            yield return new WaitForSeconds(1.5f);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("3 ESTRELLAS");
        }   
        else if (puntosConseguidos >= AvesDosEstrellas){
            yield return new WaitForSeconds(1.0f);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("2 ESTRELLAS");
        }
        else{
            yield return new WaitForSeconds(0.5f);
            anim_Estrellas.speed = 0.0f;
            Debug.Log("1 ESTRELLA");
        }
    }

    

    public void GameOver(){
        code_Resortera.enabled = false;
        ColisionesResortera.SetActive(false);

        for(int i = 0; i < cerdos.Length; i++)
            cerdos[i].enabled = false;

        anim_Derrota.Play("Derrota_Entrada");
        Debug.Log("GAME OVER !!");
    }

}