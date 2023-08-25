using UnityEngine;

using UnityEngine.Events;

public class OnTriggerEnterDo : MonoBehaviour
{
    //libertad de acciones al colisionar
    [SerializeField] protected UnityEvent alwaysActions;

    public bool IsEnabled = true;

    private GameObject colisionee;

    //protected virtual, si las clases que heredan de este script NO tienen implementado
    //el metodo, utilicen éste metodo !!!!
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        colisionee = collision.gameObject;

        alwaysActions.Invoke();
    }

    public virtual void DestroyColisionee()
    {
        if(colisionee != null)
        {
            Destroy(colisionee);
        }
    }

    //el objeto que contenga éste componente, su collider debe ser trigger
}
