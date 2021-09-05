using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.TestTools;

public class Player : SingletonMonoBehaviour<Player> {

    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    private float _xInput;
    private float _yInput;
    private State _state;

    private float _dodgeSpeed;
    private float _attackRange = 2f;
    private float _currentMovementSpeed;
    
    private Camera _mainCamera;

    private Vector3 _mousePosition;
    private Vector3 _mouseDirection;

    private Vector2 movement;

    [SerializeField] private float blockSlowMultiplicator = 0.2f;
    [SerializeField] private float maxDodgeSpeed = 8f;
    [SerializeField] private float maxMovementSpeed = 8f;
    [SerializeField] private int life;

    [SerializeField] private float dodgeStaminaCost = 30f;
    [SerializeField] private float blockStaminaCost = 5f;
    [SerializeField] private float attackStaminaCost = 20f;

    protected override void Awake () {
        base.Awake();
        _state = State.Normal;
        _mainCamera = Camera.main;
        _currentMovementSpeed = maxMovementSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {

        InputKeyHandler.Instance.OnKeyE += Instance_OnKeyE;
    }

    void Update () {
        if (_state == State.Normal || _state == State.Idle || _state == State.Blocking) {
            PlayerMovementInput();
            PlayerActionInput();
        }

        PlayerActionHandler();
        //PlayerMovementHandler();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() {
        _rigidbody2D.MovePosition(_rigidbody2D.position + movement * (_currentMovementSpeed * Time.fixedDeltaTime));
    }

    ////////////////////////////// Movement Inputs //////////////////////////////
    
    private void PlayerMovementInput() {


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        /*
        _yInput = 0f;
        _xInput = 0f;
        if (Input.GetKey(KeyCode.W)) {
            _yInput = +1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            _yInput = -1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            _xInput = -1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            _xInput = +1f;
        }
        
        if (_xInput == 0 && _yInput == 0 && _state != State.Blocking) {
            _state = State.Idle;
        }
        else {
            _state = State.Normal;
        }
        */
    }
    
    ////////////////////////////// Action Inputs //////////////////////////////

    private void PlayerActionInput() {
        if (Input.GetMouseButtonDown(0)) {
            PlayerAttack();
            return;
        }

        if (Input.GetMouseButtonDown(1)) {
            StartPlayerBlock();
            return;
        }
        
        if (Input.GetMouseButtonUp(1)) {
            EndPlayerBlock();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            DodgeInput();
            return;
        }

        if (Input.GetKey(KeyCode.Escape)) {
            // todo menu    
        }
        
        //DEBUG
        if (Input.GetKeyDown(KeyCode.K)) {
            Enemy.SpawnRandom(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            PlayerHealthSystem.Instance.TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            PlayerHealthSystem.Instance.HealDamage(1);
        }
    }


    ////////////////////////////// Interaction Inputs //////////////////////////////

    private void Instance_OnKeyE(object sender, System.EventArgs e) {

        CheckInteraction();
    }

    private void PlayerAttack() {
        if (PlayerStaminaSystem.Instance.ReduceStamina(attackStaminaCost)) {
            SoundManager.PlaySound(Sound.HeroAttack);
            this._state = State.Attacking;
            this._mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            this._mouseDirection = (_mousePosition - transform.position).normalized;
            this._mouseDirection.z = 0f;
        }
    }

    private void StartPlayerBlock() {
        if (_state == State.Dodging) return;
        if (PlayerStaminaSystem.Instance.CheckForStaminaCoast(blockStaminaCost)) {
            SoundManager.PlaySound(Sound.RaiseShield);
            PlayerStaminaSystem.Instance.ActivateShieldBlock(blockStaminaCost);
            this._currentMovementSpeed = _currentMovementSpeed * blockSlowMultiplicator;
            this._state = State.Blocking;
        } else {
            EndPlayerBlock();
        }
    }

    private void EndPlayerBlock() {
        SoundManager.PlaySound(Sound.LowerShield);
        this._currentMovementSpeed = maxMovementSpeed;
        this._state = State.Normal;
        PlayerStaminaSystem.Instance.DeactivateShieldBlock();
    }

    private void DodgeInput() {
        if (PlayerStaminaSystem.Instance.ReduceStamina(dodgeStaminaCost)) {
            this._state = State.Dodging;
            SoundManager.PlaySound(Sound.HeroDodge);
            this._mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            this._mouseDirection = _mousePosition - transform.position;
            this._mouseDirection.z = 0f;
            this._dodgeSpeed = maxDodgeSpeed;
        }
    }
    
    ////////////////////////////// Movement Handling //////////////////////////////
    
    private void PlayerMovementHandler() {

        switch (_state) {
            case State.Idle:
                // todo play idle animation
                break;
            
            case State.Normal:
                MovePlayer();
                break;
        }
    }

    private void MovePlayer() {

        Vector3 moveDir = new Vector3(_xInput, _yInput).normalized;
        if (TryMove(moveDir, _currentMovementSpeed * Time.deltaTime)) {
            //todo play walking animation
        } else {
            //todo play idle animation
        }

    }

    private bool CanMove(Vector3 dir, float distance) {
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }

    private bool TryMove(Vector3 baseMoveDir, float distance) {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        if (!canMove) {
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f && CanMove(moveDir, distance);
            if (!canMove) {
                moveDir = new Vector3(0f, baseMoveDir.y).normalized;
                canMove = moveDir.y != 0f && CanMove(moveDir, distance);
            }
        }

        if (canMove) {
            transform.position += moveDir * distance;
            return true;
        } else {
            return false;
        }
    }
    
    ////////////////////////////// Action Handling //////////////////////////////
    
    private void PlayerActionHandler() {
        switch (_state) {
            
            case State.Attacking:
                AttackAction();
                break;
            
            case State.Blocking:
                MovePlayer();
                BlockingAction();
                break;
            
            case State.Dodging:
                DodgeAction();
                break;
        }
    }

    private void AttackAction() {
        Vector3 attackPosition = transform.position + _mouseDirection * 2f;
        Enemy targetEnemy = Enemy.GetClosestEnemy(attackPosition, _attackRange);
        if (targetEnemy != null) {
            targetEnemy.TakeDamage(1);
        }
        //todo play attack animation
        this._state = State.Normal;
    }

    private void BlockingAction() {
        //todo block animation
        if (!PlayerStaminaSystem.Instance.CheckForStaminaCoast(blockStaminaCost)) {
            EndPlayerBlock();
        }
    }

    private void DodgeAction() {
        TryMove(_mouseDirection, _dodgeSpeed * Time.deltaTime);
        _dodgeSpeed -= _dodgeSpeed * 10f * Time.deltaTime;
        if (_dodgeSpeed < 1f) {
            this._state = State.Normal;
        }
    }

    ////////////////////////////// Interaction Handling //////////////////////////////

    private void CheckInteraction() {

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.3f, Vector2.zero);

        if (hits.Length > 0) {

            foreach (RaycastHit2D rc in hits) {

                if (rc.transform.GetComponent<Interactable>()) {      // Extensions don't work right now, so there are the classic calls // if (rc.IsInteractable()) {

                    rc.transform.GetComponent<Interactable>().Interact();              // rc.Interact();
                    return;
                }
            }
        }
    }
    
    ////////////////////////////// global Helper //////////////////////////////

    public State GetPlayerState() {
        return _state;
    }

}
