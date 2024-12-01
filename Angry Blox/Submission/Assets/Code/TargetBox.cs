using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    private bool scoreOnce = false;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }

    private void Scored()
    {
        // FILL ME IN
        if (scoreOnce)
        {
            return;
        }

        GetComponent<SpriteRenderer>().color = Color.green;
        ScoreKeeper.AddToScore(GetComponent<Rigidbody2D>().mass);
        scoreOnce = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !scoreOnce)
        {
            Scored();
        }

    }
}
