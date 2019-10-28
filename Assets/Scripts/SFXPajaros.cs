using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPajaros : MonoBehaviour{

    [Header("REFERENCIAS")]
    public AudioSource SFX_Pajaro;
    public AudioClip[] ClipsPajaro;

    public void GritoPajaro(){
        int aleatorio =(int) Random.Range(0.0f, 5.0f);
        SFX_Pajaro.clip = ClipsPajaro[aleatorio];
        SFX_Pajaro.Play();
    }

}