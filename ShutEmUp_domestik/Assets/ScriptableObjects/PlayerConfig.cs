using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Config", menuName ="PlayerConfig SO")]
public class PlayerConfig : ScriptableObject
{
    [Serializable]
    public class PowerConfig
    {
        //especifica el nivel de poder => 1,2,3 etc
        //segun el puntaje obtenido, se desbloquean mas disparos
        public int powerValue;
        public int cannonAmount;
    }

    public List<PowerConfig> powerConfigs;

    public PowerConfig GetPowerConfig(int powerVal)
    {
        foreach (var config in powerConfigs)
        {
            if(config.powerValue >= powerVal )
            {
                return config;
            }
        }
        Debug.LogFormat("PlayerConfig: {0}" , powerConfigs[powerConfigs.Count - 1]);
        return powerConfigs[powerConfigs.Count -1];
    }

    public int GetMaxPowerValue()
    {
        //obtener y retornar el valor del ultimo powerconfig
        //sirve para actualizar la UI, saber el maximo nivel
        var config = powerConfigs[powerConfigs.Count - 1];
        return config.powerValue;
    }
}
