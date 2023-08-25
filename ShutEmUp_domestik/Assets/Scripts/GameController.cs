
using UnityEngine;

public class GameController : MonoBehaviour
{
    // DontDestroyOnLoad() NO DESTRUIR OBJETO
    // DontDestroyOnLoad(this.gameObject);

    public static GameController Instance;

    //evento para spawner
    public delegate void EnemyDied(GameObject go);
    public event EnemyDied OnEnemyDied;

    //nuevo delegado a evento - se suscribe a este evento IUScoreController
    public delegate void ScoreChanged(int updateScore);
    public event ScoreChanged OnScoreChanged;

    //evento para contador de muertes enemigos por limites
    public delegate void EnemyKillsChanged(int updateKills);
    public event EnemyKillsChanged OnEnemyKillsChanged;



    [SerializeField] Player player;

    //obtener player para otras clases
    public Player Player { get { return player; } }


    [SerializeField] Spawner spawner;


    //int powerLevel; Lo maneja player

    private int _playerScore;

    public int PlayerScore
    {
        get
        {
            return _playerScore;
        }
        //se coloca private para que otra clase no modifique el set
        private set
        {
            _playerScore = value;
            //si hay alguien suscrito - UIScoreController
            if(OnScoreChanged != null)
            {
                OnScoreChanged.Invoke(_playerScore);
            }
        }
    }

    public bool IsGamePaused = false;

    //enemigos que se escaparon
    int enemigosSinDestruir = 0;

    int enemigosTotales = 0;

    private void Awake()
    {
        Instance = this;
    }

    //se llama OnDie desde EnemyController cuando se destruye un enemigo
    public void OnDie(GameObject deadObject, int score = 0)
    {
        PlayerScore += score;
        //Debug.LogFormat("GameController: the object {0} has died! Adding score: {1} Total score: {2}", deadObject.name, score, PlayerScore);
        player.AddToPowerLevel(1);

        //si ahy alguien suscrito, invocar (spawner esta suscrito a este evento)
        //es practicamente lo mismo que llamar por una funcion estatica (GameController.Instance...)
        //podria crearse una funcion para que detecte que enemigo murio por limites y no por un disparo
        //y podria calcularse una estadistica ej: esres un maestro de punteria!!
        OnEnemyDied?.Invoke(deadObject);
    }


    //enviado desde pickupController al colisionar con algo (player)
    //configurar las capas de colision (Layers)
    public void OnPickupPickedUp(PickupController pickupCtrl)
    {
        //Debug.LogFormat("Pickup collected! {0}", pickupCtrl);
        player.UnlockSpecial(pickupCtrl.config);

        //acumular puntaje , tambien del pickup
        PlayerScore += pickupCtrl.config.score;
    }

    public void OnPlayerDie()
    {
        Debug.Log("GameController: player died...");
    }

    ////Mio
    ///
    public void MuertePorLimites()
    {
        enemigosSinDestruir++;
        //Debug.Log("Muerte por limites, Enemigos sin destruir: "+ enemigosSinDestruir);

        //si hay alguien suscrito , hacer - (UIkillsController)
        OnEnemyKillsChanged?.Invoke(enemigosSinDestruir);

    }


    //se llama en spawner cuando se crea un enemigo
    public void EnemigoCreado()
    {
        enemigosTotales++;
        //Debug.Log("Enemigos totales: "+ enemigosTotales);
    }
}
