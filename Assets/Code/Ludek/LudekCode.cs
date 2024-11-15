using UnityEngine;

public class LudekCode : MonoBehaviour
{
    public float range;
    public float lifetime;
    public bool timePasses;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (Vector3.Distance(player.GetComponent<Rigidbody2D>().position, rb.position) <= range)
        {
            Debug.Log("Gracz w zasiegu");
            timePasses = false;
        }

        if (lifetime == 0 && timePasses)
        {
            Debug.Log("KONIEC ZYCIA");
            timePasses = false;
        }
        else
        {
            if (timePasses)
                {
                    lifetime -= 1;
                }
            }
    }
}
