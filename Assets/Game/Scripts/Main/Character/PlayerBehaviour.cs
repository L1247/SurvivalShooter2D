#region

using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

public class PlayerBehaviour : MonoBehaviour
{
#region Private Variables

    private bool       attack;
    private bool       move;
    private GameObject currentAttackingEnemy;

    private int          attackCount;
    private List<string> attackAnimations;

    private string    ANIMATION_IDLE = "Idle";
    private string    ANIMATION_MOVE = "Move";
    private string    TAG_ENEMY      = "Enemy";
    private Transform trans;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float AttackSpeed = 0.5f;

    [SerializeField]
    private int moveSpeed = 3;

#endregion

#region Unity events

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

    // Update is called once per frame
    void Update()
    {
        if (move) Move();
    }

#endregion

#region Private Methods

    private string GetAttackAnimationName()
    {
        var randomValue = Random.Range(0 , 3);
        return attackAnimations[randomValue];
    }

    private void Move()
    {
        trans.Translate(trans.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnemy(Collider2D obj)
    {
        currentAttackingEnemy = obj.gameObject;
        move                  = false;
        attack                = true;
        animator.Play(ANIMATION_IDLE);
        var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
        Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                  .TakeWhile(l => attack)
                  .Subscribe(PlayAttackAnimation);
    }

    private void PlayAttackAnimation(long obj)
    {
        attackCount++;
        animator.Play(GetAttackAnimationName());
        if (attackCount == 4)
        {
            attack      = false;
            move        = true;
            attackCount = 0;
            currentAttackingEnemy.SetActive(false);
            animator.Play(ANIMATION_MOVE);
        }
    }

#endregion
}