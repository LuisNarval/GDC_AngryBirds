using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour{

    [Header("REFERENCIAS")]
    public AudioSource SFX_Salida;
    public AudioSource SFX_Siguiente;

    private bool pausa = false;

    public void TogglePausa(){
        if (!pausa){
            pausa = true;
            Time.timeScale = 0;
            SFX_Salida.Play();
        }else{
            pausa = false;
            Time.timeScale = 1;
            SFX_Siguiente.Play();
        }
    }

}