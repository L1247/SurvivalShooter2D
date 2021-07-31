using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class CharacterMoving : MonoBehaviour
{
    private Transform trans;

    [SerializeField]
    private int moveSpeed = 3;

    [SerializeField]
    private Animator animator;

    private string       ANIMATION_IDLE = "Idle";
    private string       TAG_ENEMY      = "Enemy";
    private string       ANIMATION_MOVE = "Move";
    private bool         move;
    private List<string> attackAnimations;

    // Start is called before the first frame update
    void Start()
    {
        attackAnimations = new List<string>() { "Attack1" , "Attack2" , "Attack3" };
        trans            = transform;
        this.OnTriggerEnter2DAsObservable()
            .Where(collider2D => collider2D.CompareTag(TAG_ENEMY))
            .Subscribe(OnTriggerEnemy);
        move = true;
        animator.Play(ANIMATION_MOVE);
    }

    private void OnTriggerEnemy(Collider2D obj)
    {
        move = false;
        animator.Play(GetAttackAnimationName());
    }

    private string GetAttackAnimationName()
    {
        var randomValue = Random.Range(0 , 3);
        return attackAnimations[randomValue];
    }

    // Update is called once per frame
    void Update()
    {
        if (move) Move();
    }

    private void Move()
    {
        trans.Translate(trans.right * moveSpeed * Time.deltaTime);
    }
}