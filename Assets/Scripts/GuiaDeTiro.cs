using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaDeTiro : MonoBehaviour{

    [Header("CONFIGURACION")]
    public float VelocidadPuntos;
    public float VelocidadInicial;
    public float DistanciaNegativa;
    public float rangoDeGiro;

    [Header("REFERENCIAS")]
    public LineRenderer linea;
        
    [Header("CONSULTAS")]
    public Material mainMaterial;
    public Vector2 offSetMaterial;

    public float gravedad = 9.8f;
    public float angulo;
    public float tiempo;
    public float velocidad;


    // Start is called before the first frame update
    void Start(){
        mainMaterial = this.GetComponent<LineRenderer>().material;
        offSetMaterial = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update(){
        DesplazarPuntos();
    }

    void DesplazarPuntos(){
        offSetMaterial.x += VelocidadPuntos *Time.deltaTime;
        mainMaterial.SetTextureOffset("_MainTex", offSetMaterial);
    }

    public void CalcularTiempo(float a, float v){
        angulo = a;
        velocidad = v;

        tiempo = (2 * velocidad * Mathf.Sin(angulo)) / gravedad;
        Debug.Log("Tiempo : " + tiempo);

        Invoke("Alarma", tiempo);
    }

  


    public void CalcularAngulo(Vector3 posAve, Vector3 posCentral){
        this.transform.position = posAve;

        float catetoAdyascente = posCentral.x - posAve.x;
        float catetoOpuesto = posCentral.y - posAve.y;
        float angulo = Mathf.Atan(catetoOpuesto / catetoAdyascente);

        if (catetoOpuesto > 0){
            if(Mathf.Rad2Deg * angulo < rangoDeGiro && Mathf.Rad2Deg * angulo > -rangoDeGiro)
                TiroAlPiso(posAve, posCentral);
            else
                CalcularTrayectoria(angulo);
        }
        else{
           TiroAlPiso(posAve, posCentral);
        }

    }


    public void CalcularTrayectoria(float ang){
        //float xMaxima = ((VelocidadInicial * VelocidadInicial) * Mathf.Sin(2 * ang)) / 9.8f;
        //float yMaxima = ( (VelocidadInicial * VelocidadInicial) * (Mathf.Sin(ang) * Mathf.Sin(ang)) )  / (2*9.8f)  ;

        float tiempo = ( 2 * VelocidadInicial * Mathf.Sin(ang) ) / 9.8f;

        float tiempoRelativo;
        float x;
        float y;

        for (int i = 0; i < linea.positionCount; i++){
            tiempoRelativo = (tiempo/linea.positionCount)*i;

            x = VelocidadInicial * Mathf.Cos(ang) * tiempoRelativo;
            y = (VelocidadInicial * Mathf.Sin(ang) * tiempoRelativo) - ((9.8f * tiempoRelativo * tiempoRelativo) / 2);

            linea.SetPosition(i, new Vector3(x, y, 0.0f));
        }

    }


    public void TiroAlPiso(Vector3 pInicial, Vector3 pCentral){

        Vector3 pFinal = pCentral - pInicial;
        pFinal *= DistanciaNegativa;
        Debug.DrawLine(pInicial,pInicial+pFinal);

        float x;
        float y;


        for (int i = 0; i<linea.positionCount ; i++){
            x = pInicial.x+pFinal.x / 10*i;
            y = (((pInicial.y+ pFinal.y - pInicial.y) / (pInicial.x+pFinal.x - pInicial.x)) * (x - pInicial.x)) + pInicial.y;

            linea.SetPosition(i, new Vector3( x - this.transform.position.x, y - this.transform.position.y, 0.0f));
        }

    }


}