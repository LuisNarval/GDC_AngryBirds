using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{

    [Header("CONFIGURACIONES")]
    public float velocidad;
    [Header("REFERENCIAS")]
    public RectTransform pareja;

    private RectTransform RT;

    // Start is called before the first frame update
    void Start(){
        RT = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update(){
        movimiento();
    }

    private void LateUpdate(){
        respawn();
    }

    void movimiento(){
        RT.Translate(Vector3.right*velocidad*Time.deltaTime);
    }

    void respawn(){
        //Debug.Log("Rect: " + RT.localPosition.x);
        if (RT.localPosition.x <= -1020){
            RT.localPosition = new Vector3(pareja.localPosition.x+RT.sizeDelta.x,
                                           RT.localPosition.y,RT.localPosition.z);
        }
    }





}
