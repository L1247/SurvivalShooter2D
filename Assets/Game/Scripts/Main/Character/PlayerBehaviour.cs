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
    private bool      move;
    private Character currentAttackingEnemy;

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

    public override void OntriggerEnter(Character target)
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

    public override void OntriggerExit(Character target)
    {
        attack      = false;
        move        = true;
        attackCount = 0;
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
        attackCount++;
        animator.Play(GetAttackAnimationName());
        currentAttackingEnemy.TakeDamage(25);
    }

#endregion
}