using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBreaker : MonoBehaviour{

    [Header("CONFIGURACIONES")]
    public string SIGUIENTE_NIVEL;

    [Header("REFERENCIAS")]
    public GameObject BtnSiguiente1;
    public GameObject BtnSiguiente2;

    // Start is called before the first frame update
    void Start(){
        VerificarSiguienteNivel();
    }

    public void VerificarSiguienteNivel(){
        if (PlayerPrefs.HasKey(SIGUIENTE_NIVEL)){
            BtnSiguiente1.SetActive(true);
            BtnSiguiente2.SetActive(true);
        }else{
            BtnSiguiente1.SetActive(false);
            BtnSiguiente2.SetActive(false);
        }
    }

    public void AbrirSiguienteNivel(){
        PlayerPrefs.SetString(SIGUIENTE_NIVEL, "OK");
        VerificarSiguienteNivel();
    }


}