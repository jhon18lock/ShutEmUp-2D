using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboardListener : MonoBehaviour, IInputeable
{
    //se coloca como un GO en la escena a utilizar

    //motodos heredados de IInputeable (Interface)
    //Escucha las entradas del teclado y envia los parametros a InputProvider
    public void ShootPressed()
    {
        InputProvider.TriggerOnHasShoot();
    }

    public void GetDirection(Vector3 direction)
    {
        InputProvider.TriggerDirection(direction);
    }

    
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            ShootPressed();
        }

        GetDirection(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
