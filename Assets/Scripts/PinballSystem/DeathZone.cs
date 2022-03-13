using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float mAddForce = 1f;

    Transform player;
    Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        player = Manipulator.Instance.PlayerPos;
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == player)
        {
            Debug.Log("collision:" + collision.gameObject.name);
            Vector2 force = new Vector2((Random.Range(0, 100) % 2 == 0 ? 1 : -1) * mAddForce, mAddForce);
            playerRB.AddForce(force);
        }
    }
}
