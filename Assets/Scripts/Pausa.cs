using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour{

    [Header("REFERENCIAS")]
    public AudioSource SFX_Salida;
    public AudioSource SFX_Siguiente;
    public GameObject PantallaPausa;
    private bool pausa = false;
    BGMController BGM;

    private void Start(){
        PantallaPausa.SetActive(false);
        BGM = GameObject.Find("BGM").GetComponent<BGMController>();        
    }

    public void TogglePausa(){
        if (!pausa){
            pausa = true;
            PantallaPausa.SetActive(true);
            BGM.GetComponent<AudioSource>().volume = 0.15f;
            Time.timeScale = 0;
            SFX_Salida.Play();
        }
        else{
            pausa = false;
            PantallaPausa.SetActive(false);
            BGM.GetComponent<AudioSource>().volume = BGM.volumenMaximo;
            Time.timeScale = 1;
            SFX_Siguiente.Play();
        }
    }

}