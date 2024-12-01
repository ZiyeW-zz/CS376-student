using UnityEngine;

/// <summary>
/// Controls the behavior of an on-screen enemy.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// How fast the enemy can accelerate
    /// </summary>
    public float EnginePower = 1;

    /// <summary>
    /// How close it tries to get to the player
    /// </summary>
    public float ApproachDistance = 5;

    /// <summary>
    /// How fast the orbs it fires should move
    /// </summary>
    public float OrbVelocity = 10;

    /// <summary>
    /// How heavy the orbs it fires should be
    /// </summary>
    public float OrbMass = .5f;

    /// <summary>
    /// Period the enemy should wait between shots
    /// </summary>
    public float CoolDownTime = 1;

    //field that tracks when the next spawn should happen
    public float SpawnTracker = 0;

    /// <summary>
    /// Prefab for the orb it fires
    /// </summary>
    public GameObject OrbPrefab;

    /// <summary>
    /// Transform from the player object
    /// Used to find the player's position
    /// </summary>
    private Transform player;

    /// <summary>
    /// Our rigid body component
    /// Used to apply forces so we can move around
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Vector from us to the player
    /// </summary>
    private Vector2 OffsetToPlayer => player.position - transform.position;

    /// <summary>
    /// Unit vector in the direction of the player, relative to us
    /// </summary>
    private Vector2 HeadingToPlayer => OffsetToPlayer.normalized;



    /// <summary>
    /// Initialize player and rigidBody fields
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Fire if it's time to do so
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Update() 
    {
        // TODO
        if (Time.time > SpawnTracker)
        {
            SpawnTracker = Time.time + CoolDownTime;
            Fire();
        }
    }

    /// <summary>
    /// Make a new orb, place it next to us, but shifted one unit in the direction of the player
    /// Set its mass to OrbMass and its velocity to OrbVelocity units per second, in the direction of the player
    /// </summary>
    private void Fire()
    {
        // TODO
        //Vector2 orbPos = new(RB.position.x + transform.right.x, RB.position.y);

        Vector2 direction = new (rigidBody.position.x + HeadingToPlayer.x, rigidBody.position.y + HeadingToPlayer.y);
        GameObject enemyOrb = Instantiate(OrbPrefab, direction, Quaternion.identity);
        enemyOrb.GetComponent<Rigidbody2D>().velocity = OrbVelocity * HeadingToPlayer;
        enemyOrb.GetComponent<Rigidbody2D>().mass = OrbMass;


    }

    /// <summary>
    /// Accelerate in the direction of the player, unless we're nearby
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        var offsetToPlayer = OffsetToPlayer;
        var distanceToPlayer = offsetToPlayer.magnitude;
        var controlSign = distanceToPlayer > ApproachDistance ? 1 : -1; 
        rigidBody.AddForce(controlSign * (EnginePower / distanceToPlayer) * offsetToPlayer);
    }

    /// <summary>
    /// If this is called, we're off screen, so give the player a point.
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void OnBecameInvisible()
    {
        ScoreKeeper.ScorePoints(1);
    }
}
