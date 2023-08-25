using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ejecutar en Modo edicion
[ExecuteInEditMode]


public class Celda : MonoBehaviour
{
    private void Update()
    {
        float tamx = transform.localScale.x;
        float tamy = transform.localScale.y;

        float posx = transform.localPosition.x;
        float posy = transform.localPosition.y;

        float posicionSnapX = Mathf.RoundToInt(posx / tamx) * tamx;
        float posicionSnapY = Mathf.RoundToInt(posy / tamy) * tamy;

        transform.localPosition = new Vector3(posicionSnapX, posicionSnapY, 0);
    }
}