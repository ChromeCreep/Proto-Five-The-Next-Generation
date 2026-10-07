using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    [SerializeField] private int maxHits = 3;
    private int hitCount = 0;

    public float velocity = 10;
    public float velX = 0f;
    public float velY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.tag == "P1")
        {
            Debug.Log("P1 hit P2!");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Hit!");
                Destroy(other.gameObject);
            }
        }
        
        if (this.gameObject.tag == "P2")
        {
            Debug.Log("P2 hit P1!");
            if (other.gameObject.CompareTag("Gamepad Player"))
            {
                Debug.Log("Hit!");
                Destroy(other.gameObject);
            }
        }
    }
}
