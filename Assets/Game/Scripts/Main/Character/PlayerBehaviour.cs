#region

using System;
using System.Collections.Generic;
using Main.Character;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

public class PlayerBehaviour : CharacterBehaviour
{
#region Private Variables

    private bool      attack;
    private bool      isDead;
    private bool      move;
    private Character currentAttackingEnemy;

    private List<string> attackAnimations;

    private readonly string ANIMATION_DIE = "Death";

    private string    ANIMATION_IDLE = "Idle";
    private string    ANIMATION_MOVE = "Move";
    private Transform trans;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float AttackSpeed = 0.5f;

    [SerializeField]
    private int damage = 10;

    [SerializeField]
    private int moveSpeed = 3;

#endregion

#region Unity events

    // Start is called before the first frame update
    void Start()
    {
        attackAnimations = new List<string>() { "Attack1" , "Attack2" , "Attack3" };
        trans            = transform;
        move             = true;
        animator.Play(ANIMATION_MOVE);
    }

    // Update is called once per frame
    void Update()
    {
        if (move) Move();
    }

#endregion

#region Public Methods

    public override void MakeCharacterDie()
    {
        move   = false;
        attack = false;
        isDead = true;
        animator.Play(ANIMATION_DIE);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void TriggerEnter(Character target)
    {
        currentAttackingEnemy = target;
        move                  = false;
        attack                = true;
        animator.Play(ANIMATION_IDLE);
        var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
        Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                  .TakeWhile(l => attack)
                  .Subscribe(PlayAttackAnimation).AddTo(gameObject);
    }

    public override void TriggerExit(Character target)
    {
        if (isDead) return;
        attack = false;
        move   = true;
        animator.Play(ANIMATION_MOVE);
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

    private void PlayAttackAnimation(long obj)
    {
        animator.Play(GetAttackAnimationName());
        currentAttackingEnemy.TakeDamage(damage);
    }

#endregion
}