using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rastro : MonoBehaviour{

    [Header("CONFIGURACIONES")]
    public float Frecuencia;

    [Header("REFERENCIAS")]
    public Transform[] Rastros;

    public void DejarNuevoRastro(Transform objetivo){
        StopAllCoroutines();
        eliminarRastro();
        StartCoroutine(corrutina_DejarRastros(objetivo));
    }

    void eliminarRastro(){
        for (int i = 0; i< Rastros.Length; i++){
            Rastros[i].position = Vector3.one * -300;
        }
    }

    IEnumerator corrutina_DejarRastros(Transform objetivo){
        yield return new WaitForSeconds(Frecuencia);
        for(int i = 0; i < Rastros.Length; i++){
            Rastros[i].position = objetivo.position;
            yield return new WaitForSeconds(Frecuencia);
        }
    }

}