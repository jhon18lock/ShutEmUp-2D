
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet config", menuName = "Bullet Config SO")]
public class BulletConfig : ScriptableObject
{
    public int playerID;
    public float damage;
    public float speed;
}
