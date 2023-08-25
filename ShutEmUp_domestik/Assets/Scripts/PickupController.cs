using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public PickupConfig config;

    //si me colectan a mi, informar al GameController
    //enviandome a mi como parametro
    //se llama a si mismo en los eventos de colision - ontriggerenterdo
    public void OnPickedUp()
    {
        GameController.Instance.OnPickupPickedUp(this);
    }
    
}
