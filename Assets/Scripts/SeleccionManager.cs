using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SeleccionManager : MonoBehaviour {

    [Header("REFERENCIAS")]
    public Animator anim_Seleccion;

    private string SiguienteEscena;

    public void CargarEscena(string escena) {
        SiguienteEscena = escena;
        anim_Seleccion.Play("Seleccion_Salida");
        Invoke("cargar", 1.5f);
    }

    private void cargar() {
        SceneManager.LoadScene(SiguienteEscena);
    }

}