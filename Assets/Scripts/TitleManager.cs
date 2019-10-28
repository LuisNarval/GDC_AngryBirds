using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour{

    [Header("REFERENCIAS")]
    public Animator anim_Title;
    public Transform botonPlay;
    public AudioSource BGM;
    public Image cortina;

    public void Awake(){
        PlayerPrefs.SetString("NivelUno", "OK");
    }

    public void Play(string next){
        //anim_Title.Play("Titulo_Salida");
        StartCoroutine(corrutina_BajarVolumen(next));
    }

    public void agrandarBoton(){
        botonPlay.localScale *= 1.2f;
    }

    public void encogerBoton(){
        botonPlay.localScale /= 1.2f;
    }

    IEnumerator corrutina_BajarVolumen(string n){
        float tiempo = 1;

        cortina.raycastTarget = true;

        while (tiempo > 0){
            tiempo -= Time.deltaTime;
            BGM.volume = tiempo;

            cortina.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - tiempo);

            yield return new WaitForEndOfFrame();
        }

        cortina.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        yield return new WaitForSeconds(1.0f);

        Cargar(n);
    }

    void Cargar(string SiguienteEscena){
        SceneManager.LoadScene(SiguienteEscena);
    }

}