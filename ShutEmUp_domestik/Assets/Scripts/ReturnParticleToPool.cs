using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticleToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    // las particulas deben contener este script
    // En su propiedad StopAction debe ser CallBack
}
