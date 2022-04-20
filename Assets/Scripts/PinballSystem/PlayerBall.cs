using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float Skill_Strength = 1000;
    public bool SkillCoolDown;

    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Manipulator.Instance.PlayerPos = transform;
    }

    public void Skill_NavigationFlip()
    {
        if (!SkillCoolDown) return;
        if (!Manipulator.Instance.transform) return;
        Vector2 dir = Manipulator.Instance.transform.position - transform.position;
        rb2D.AddForce(dir.normalized * Skill_Strength);
        SkillCoolDown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TigerMasterData>(out TigerMasterData data))
        {
            Debug.Log($"Player Hit: {collision.gameObject.name}");
            AudioManager.Instance.Play("PlayerHit");
            TigerMasterSystem.Instance.GetHurt(data, UnityEngine.Random.Range(10, 150));
            Manipulator.Instance.MainCamera.DOShakePosition(0.1f, 0.05f);
            return;
        }
        else if (collision.gameObject.TryGetComponent<Flip>(out Flip flip))
        {
            SkillCoolDown = true;
        }
        AudioManager.Instance.Play("PlayerHitNothing");
    }
}
