﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    [Header("CONFIGURACIONES")]

    public float SensibilidadX;

    public Vector3 posicionInicial;
    public float anchoFocalInicial;
    public Vector3 posicionMovimiento;
    public float anchoFocalMovimiento;
    public float anchoFocalEnCerdos;
    public float anchoFocalEnPajaros;

    [Header("Velocidades")]
    public float tiempoACerdos;
    public float tiempoAPajaros;
    public float tiempoANormalidad;

    [Header("Máximos")]
    public float maxD;
    public float maxI;

    [Header("REFERENCIAS")]
    public Camera camara;
    public Transform posCerdos;
    public Transform posPajaros;
    public CanvasGroup HUD;

    [Header("CONSULTAS")]
    public float tiempo;
    public bool Activo = false;
    public bool EnDrag = false;

    private float posX = 0;


    // Start is called before the first frame update
    void Start() {
        HUD.alpha = 0.0f;
        HUD.blocksRaycasts = false;
        camara.transform.position = posicionInicial;
        camara.orthographicSize = anchoFocalInicial;
        StartCoroutine(corrutina_TomaDeApertura());
    }

    IEnumerator corrutina_TomaDeApertura(){
        yield return new WaitForSeconds(1.5f);
        Debug.Log("A CERDOS!");
        tiempo = 0;
        while (tiempo < 1.5f) {
            camara.transform.position = new Vector3(Mathf.Lerp(posicionInicial.x, posCerdos.position.x, tiempo),
                                                    Mathf.Lerp(posicionInicial.y, posCerdos.position.y, tiempo), camara.transform.position.z);

            tiempo += Time.deltaTime * 1.0f / tiempoACerdos;
            camara.orthographicSize = Mathf.Lerp(anchoFocalInicial, anchoFocalEnCerdos, tiempo/1.0f);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        Debug.Log("A PAJAROS!");
        tiempo = 0;
        while (tiempo < 1.5f){
            camara.transform.position = new Vector3(Mathf.Lerp(posCerdos.position.x, posPajaros.position.x, tiempo),
                                                    Mathf.Lerp(posCerdos.position.y, posPajaros.position.y, tiempo), camara.transform.position.z);

            tiempo += Time.deltaTime * 1.0f / tiempoAPajaros;
            camara.orthographicSize = Mathf.Lerp(anchoFocalEnCerdos, anchoFocalEnPajaros, tiempo / 1.0f);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        Debug.Log("A TAMAÑO FINAL");
        tiempo = 0;
        while (tiempo < 1.0f){
            camara.transform.position = new Vector3(Mathf.Lerp(posPajaros.position.x, posicionMovimiento.x, tiempo),
                                                    Mathf.Lerp(posPajaros.position.y, posicionMovimiento.y, tiempo), camara.transform.position.z);

            tiempo += Time.deltaTime * 1.0f / tiempoANormalidad;
            camara.orthographicSize = Mathf.Lerp(anchoFocalEnPajaros, anchoFocalMovimiento, tiempo / 1.0f);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("COLOCAR HUD");
        tiempo = 0;
        while (tiempo < 1.0f){
            HUD.alpha = tiempo;
            tiempo += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        HUD.alpha = 1.0f;
        HUD.blocksRaycasts = true;

        Activo = true;
    }



    public void InicioDrag(){
        if (Activo)
            EnDrag = true;
    }

    public void FinDrag(){
        if (Activo && EnDrag){
            EnDrag = false;
            posX = 0;
        }

    }

    public void Drag(){
        if (EnDrag){
            if(posX != 0){
                float movX = Input.mousePosition.x - posX;
                moverCamara(movX);
            }
            posX = Input.mousePosition.x;
        }
    }

    void moverCamara(float cantidad){
        if (cantidad > 0 &&  camara.transform.position.x > maxI)
            camara.transform.Translate(Vector3.right * cantidad * SensibilidadX * Time.deltaTime);
        else if(cantidad < 0 && camara.transform.position.x < maxD)
            camara.transform.Translate(Vector3.right * cantidad * SensibilidadX * Time.deltaTime);

        if (camara.transform.position.x > maxD)
            camara.transform.position = new Vector3(maxD, camara.transform.position.y, camara.transform.position.z);

        if (camara.transform.position.x < maxI)
            camara.transform.position = new Vector3(maxI, camara.transform.position.y, camara.transform.position.z);
    }


}