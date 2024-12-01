using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    public GameObject ExplosionPrefab;

    private void Destruct()
    {
        Destroy(gameObject);
    }

    private void Boom()
    {
        var pointEffector = GetComponent<PointEffector2D>();
        pointEffector.enabled = true;

        var boxSpriteRenderer = GetComponent<SpriteRenderer>();
        boxSpriteRenderer.enabled = false;

        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);

        Invoke("Destruct", 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normalImpulse >= ThresholdImpulse)
            {
                Boom();
                break;
            }
        }
    }


}
