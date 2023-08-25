using UnityEngine;

//clase estatica accesible
//InputKeyboardListener envia info a éste script
public static class InputProvider
{
    //eventos
    public delegate void HasShoot();
    public static event HasShoot OnHasShoot;

    public delegate void Direction(Vector3 direction);
    public static event Direction OnDirection;

    //si hay alguien suscrito a estos metodos - invocar
    //ej: Player

    public static void TriggerOnHasShoot()
    {
        //OnHasShoot?.Invoke();

        if(OnHasShoot != null)
        {
            OnHasShoot.Invoke();
        }
    }

    
    public static void TriggerDirection(Vector3 direction)
    {
        //OnDirection?.Invoke(direction);

        if (OnDirection != null)
        {
            OnDirection.Invoke(direction);
        }
    }
}