using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialsController : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] GameObject shield;

    [SerializeField] private UnityEvent actionsOnFinish;

    private Coroutine laserCoroutine;
    private Coroutine shieldCoroutine;


    //Enviado de Player
    //segun el pickup activa los GameObjects
    public void UnlockSpecial(PickupConfig config)
    {
        Debug.LogFormat("SpecialsController: Special collected type: {0}", config.type);

        switch (config.type)
        {
            case PickupType.Laser:
                //en caso de que se vuelva a tomar un pickup
                if(laserCoroutine != null)
                {

                    StopCoroutine(laserCoroutine);
                }
                laser.SetActive(true);
                laserCoroutine = StartCoroutine(DisableAfterSeconds(laser, config.durationInSeconds));
                break;
            case PickupType.Shield:
                if(shieldCoroutine != null)
                {

                    StopCoroutine(shieldCoroutine);
                }
                shield.SetActive(true);
                // instruccion lambda que permite llamar a un evento desde la configuracion de specialController
                //ej: para volver a activar el colider del player al terminar la duracion del pickup
                shieldCoroutine = StartCoroutine(DisableAfterSeconds(shield, config.durationInSeconds, () => { if (actionsOnFinish != null) { actionsOnFinish.Invoke(); } }));
                break;
            default:
                break;
        }
    }

    //corrutina para desactivar objeto o pickup
    //  system.Action ->> accion al terminar

    IEnumerator DisableAfterSeconds(GameObject objectToDisable, float time, System.Action onFinish = null)
    {
        yield return new WaitForSeconds(time);
        objectToDisable.SetActive(false);

        //volver a activar colider de player
        if(onFinish != null)
        {
            onFinish.Invoke();
        }
    }
}
