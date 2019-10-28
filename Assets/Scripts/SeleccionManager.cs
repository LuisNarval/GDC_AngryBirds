using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SeleccionManager : MonoBehaviour {

    [Header("REFERENCIAS")]
    public Animator anim_Seleccion;
    public AudioSource BGM;

    private string SiguienteEscena;

    public void CargarEscena(string escena) {
        SiguienteEscena = escena;
        anim_Seleccion.Play("Seleccion_Salida");
        Invoke("cargar", 1.5f);
        StartCoroutine(corrutina_BajarVolumen());
    }

    private void cargar() {
        SceneManager.LoadScene(SiguienteEscena);
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