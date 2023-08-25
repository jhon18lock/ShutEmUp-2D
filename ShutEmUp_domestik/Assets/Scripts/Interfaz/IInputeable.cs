//interface : se debe implementar en los scripts que lo requieran
//provee metodos o variables que usarian varios scripts
//estos metodo s o variables se deben implementar en cada script
//ej InputKeyboardListener

using UnityEngine;

public interface IInputeable
{
    void ShootPressed();

    void GetDirection(Vector3 direction);
}
