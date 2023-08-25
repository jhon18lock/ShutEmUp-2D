using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    // utilizar un contenedor padre para los fondos
    //utilizar un quad y expandir el tamaño a utilizar
    //si se elije una textura(sprite) cambiar su propiedad CLAMP a REPEAT
    //para que se vea la imagen, en su shader cambiar a - UNLIT , TEXTURE


    public float scrollSpeed = 0.3f;

    [SerializeField] private MeshRenderer mesh;
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);

        //modificar su offset a traves del material
        mesh.material.mainTextureOffset = offset;
    }
}
