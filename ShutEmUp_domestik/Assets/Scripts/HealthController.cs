
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public float health;

    float maxHealth = 5f;


    private void OnEnable()
    {
        health = maxHealth;
    }

    //dejar visible en editor para elegir acciones
    [SerializeField] UnityEvent onZeroHealthActions;

    [SerializeField] UnityEvent onDamageActions;

    public void OnDamage(float damageAmount)
    {
        health -= damageAmount;

        //Debug.LogFormat("OnDamage recived: {0}. Current health; {1}", damageAmount, health);
        if(health <= 0)
        {
            OnZeroHealth();
            return;
        }
        onDamageActions.Invoke();
    }

    public void OnZeroHealth()
    {
        //Debug.Log("Estoy destruido!!" + gameObject.name);
        //si hay eventos , preceder
        if(onZeroHealthActions != null)
        {
            onZeroHealthActions.Invoke();
        }
    }

    public void SetHealth(float value)
    {
        // refactorizar con una vida axima, NO setear un valor directamente
        health = value;


    }
}
