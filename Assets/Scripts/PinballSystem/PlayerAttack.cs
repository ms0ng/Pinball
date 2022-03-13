using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TigerMasterData>(out TigerMasterData data))
        {
            Debug.Log($"Player Attack: {collision.gameObject.name}");
            TigerMasterSystem.Instance.GetHurt(data, 100);
        }

    }
}
