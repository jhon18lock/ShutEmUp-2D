using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase por fuera de MonoBehaviour
//solo necesita contener valores

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    public Mover moverComponent;
    public Boundary boundary;

    //shooters independientes
    [SerializeField] List<Shooter> shooters;



    [SerializeField] PlayerConfig config;

    [SerializeField] SpecialsController specialsController;

    //referencia para desactivar collider
    [SerializeField] private Collider2D playerCollider;

    //delegado para UIPowerController
    public delegate void PowerChanged(int currentPower, int totalPower);
    public event PowerChanged OnPowerChanged;

    int powerLevel;
    int unlockedCannons = 1;


    // Start is called before the first frame update
    void Start()
    {
        moverComponent.speed = speed;

        //manera de ceder metodos
        //accediendo a singleton
        //suscribirse a eventos
        InputProvider.OnHasShoot += OnHasShoot;
        InputProvider.OnDirection += OnDirection;

    }


    //suscrito a inputProvider

    // activar algun power up
    private void OnHasShoot()
    {
        //instanciar desde un punto especifico(shootOrigin
        //y en relacion a la rotacion de shootorigin
        //utilizar quaternion.identity para ignorar rotacion del punto de instancia
        //Instantiate(shootPrefab, shootOrigin.position, shootOrigin.rotation);

        //instanciar como hijo de Player
        //Instantiate(shootPrefab, shootOrigin, false);

        //////foreach (var shooter in shooters)
        //////{
        //////    shooter.DoShoot();
        //////}



        //shooters pool
        // configurar 3 shooterPool - o como indique en el player config
        //sino tira error por el index al recorrer la lista
        //for (int i = 0; i < unlockedCannons; i++)
        //{
        //    var shooter = shootersP[i];
        //    shooter.DoShoot();
        //}

        /*
        if (cooldownShootTime > timeToNextShoot)
        {
            cooldownShootTime = 0f;

            for (int i = 0; i < unlockedCannons; i++)
            {
                var shooter = shooters[i];
                shooter.DoShoot();
            }
        }
        */

    }

    //suscrito a inputProvider
    private void OnDirection(Vector3 direction)
    {
        moverComponent.direction = direction;
    }



    //lo envia gameController, que recibe de enemycontroller
    public void AddToPowerLevel(int powerToAdd)
    {
        powerLevel += powerToAdd;
        //se almacena el PlayerConfig en variable y se envia
        //el nivel actual
        var powerConfig = config.GetPowerConfig(powerLevel);
        //segun el puntaje que se evalua en PlayerConfig, activa o no mas cañones
        unlockedCannons = powerConfig.cannonAmount;

        Debug.LogFormat("Player has {0} cannons unlocked. Current powerLevel: {1}", unlockedCannons,powerLevel);

        //enviar info a UIPowerController
        //con parametros a evento suscrito - se debe suscribir a nuestros eventos
        //Si hay alguien suscrito, invocar
        //nuestro valor actual y el valor maximo de playerconfig
        if(OnPowerChanged != null)
        {
            OnPowerChanged.Invoke(powerLevel, config.GetMaxPowerValue());
        }
    }

    public void OnPlayerDie()
    {
        GameController.Instance.OnPlayerDie();
    }


    //enviado desde gamecontroller
    //gamecontroller lo recibe de pickupcontroller
    public void UnlockSpecial(PickupConfig pickup)
    {
        Debug.LogFormat("Special collected type: {0}", pickup.type);
        specialsController.UnlockSpecial(pickup);

        //si el pickup es shield, desactivar collider de player
        if(pickup.type == PickupType.Shield)
        {
            EnableCollider(false);
        }
    }

    //setear colider de player
    //volver a activar desde inspector en los eventos de healthController
    //que contiene shield GameObject del player
    public void EnableCollider(bool shouldEnable)
    {
        playerCollider.enabled = shouldEnable;
    }

    //metodos para UIPowerController a traves de GameController
    public int GetCurrentPowerLevel()
    {
        return powerLevel;
    }
    public int GetMaxPowerLevel()
    {
        return config.GetMaxPowerValue();
    }


    // Update is called once per frame
    void Update()
    {
        //limites de la nave
        //x:8   y:5
        float x = Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax);
        float y = Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax);
        transform.position = new Vector3(x, y);

    }
}
