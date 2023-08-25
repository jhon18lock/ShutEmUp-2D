using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsV1 : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;

    public float velocidad = 2f;
    public float distanciaCambio = 0.2f;
    byte siguuentePosicion = 0;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            wayPoints[siguuentePosicion].transform.position,
            velocidad * Time.deltaTime);

        if(Vector3.Distance(transform.position, 
            wayPoints[siguuentePosicion].transform.position) < distanciaCambio)
        {
            siguuentePosicion++;
            if(siguuentePosicion >= wayPoints.Count)
            {
                siguuentePosicion = 0;
            }
        }
    }
}
