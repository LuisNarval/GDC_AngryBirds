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
    public AudioSource BGM;
    public void Play(){
        anim_Title.Play("Titulo_Salida");
        Invoke("Cargar", 2.0f);
        StartCoroutine(corrutina_BajarVolumen());
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

    IEnumerator corrutina_BajarVolumen(){
        float tiempo = 1;

        while (tiempo > 0){
            tiempo -= Time.deltaTime;
            BGM.volume = tiempo;

            yield return new WaitForEndOfFrame();
        }
    }

}