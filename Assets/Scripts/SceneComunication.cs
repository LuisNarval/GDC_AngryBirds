using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneComunication : MonoBehaviour{
    [Header("CONFIGURACION")]
    public string EscenaSiguiente;

    [Header("REFERENCIAS")]
    public Animator anim_Cortinas;

    [Header("CONSULTA")]
    public string EscenaActual;
   

    // Start is called before the first frame update
    void Start(){
        EscenaActual = SceneManager.GetActiveScene().name;
       
    }


    public void SeleecionarNivel(){
        StartCoroutine(corrutina_CargarEscena("Seleccion"));
    }

    public void Reiniciar(){
        StartCoroutine(corrutina_CargarEscena(EscenaActual));
    }

    public void Siguiente(){
        StartCoroutine(corrutina_CargarEscena(EscenaSiguiente));
    }


    IEnumerator corrutina_CargarEscena(string escena){
        anim_Cortinas.Play("Cerrar");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(escena);
    }

}