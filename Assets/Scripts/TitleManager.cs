using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour{

    [Header("CONFIGURACION")]
    public string SiguienteEscena;

    [Header("REFERENCIAS")]
    public Animator anim_Title;
    public Transform botonPlay;

    public void Play(){
        anim_Title.Play("Titulo_Salida");
        Invoke("Cargar", 2.0f);
    }

    void Cargar(){
        SceneManager.LoadScene(SiguienteEscena);
    }


    public void agrandarBoton(){
        botonPlay.localScale *= 1.2f;
    }

    public void encogerBoton(){
        botonPlay.localScale /= 1.2f;
    }

}


