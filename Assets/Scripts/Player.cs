using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class Player : SingletonMonoBehaviour<Player> {

    private Rigidbody2D _rigidbody2D;
    private float _xInput;
    private float _yInput;
    private State state;

    private float dodgeSpeed;
    private float attackRange = 2f;
    
    private Camera _mainCamera;

    private Vector3 _mousePosition;
    private Vector3 _mouseDirection;

    [SerializeField] private float maxDodgeSpeed = 8f;
    [SerializeField] private float maxMovementSpeed = 8f;
    [SerializeField] private int life;
    private float _currentMovementSpeed;
    
    // Start is called before the first frame update
    protected override void Awake () {
        base.Awake();
        state = State.Normal;
        _mainCamera = Camera.main;
        _currentMovementSpeed = maxMovementSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (state == State.Normal || state == State.Idle) {
            PlayerMovementInput();
            PlayerActionInput();
        }

        PlayerActionHandler();
        PlayerMovementHandler();
    }

    ////////////////////////////// Movement Inputs //////////////////////////////
    
    private void PlayerMovementInput() {
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
        
        if (_xInput == 0 && _yInput == 0) {
            state = State.Idle;
        }
        else {
            state = State.Normal;
        }
    }
    
    ////////////////////////////// Action Inputs //////////////////////////////

    private void PlayerActionInput() {
        if (Input.GetMouseButtonDown(0)) {
            PlayerAttack();
        }

        if (Input.GetMouseButtonDown(1)) {
            // todo block
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DodgeInput();
        }
        
        if (Input.GetKey(KeyCode.Escape)) {
            // todo menu    
        }
        
        //DEBUG
        if (Input.GetKeyDown(KeyCode.K)) {
            Enemy.SpawnRandom(transform.position);
        }
    }

    private void PlayerAttack() {
        this.state = State.Attacking;
        this._mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        this._mouseDirection = (_mousePosition - transform.position).normalized;
        this._mouseDirection.z = 0f;
    }
    
    private void DodgeInput() {
        this.state = State.Dodging;
        this._mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        this._mouseDirection = _mousePosition - transform.position;
        this._mouseDirection.z = 0f;
        this.dodgeSpeed = maxDodgeSpeed;
    }
    
    ////////////////////////////// Movement Handling //////////////////////////////
    
    private void PlayerMovementHandler() {
        switch (state) {
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
        switch (state) {
            
            case State.Attacking:
                AttackAction();
                break;
            
            case State.Dodging:
                DodgeAction();
                break;
        }
    }

    private void AttackAction() {
        Vector3 attackPosition = transform.position + _mouseDirection * 2f;
        Enemy targetEnemy = Enemy.GetClosestEnemy(attackPosition, attackRange);
        if (targetEnemy != null) {
            targetEnemy.TakeDamage(1);
        }
        //todo play attack animation
        this.state = State.Normal;
    }

    private void DodgeAction() {
        TryMove(_mouseDirection, dodgeSpeed * Time.deltaTime);
        dodgeSpeed -= dodgeSpeed * 10f * Time.deltaTime;
        if (dodgeSpeed < 1f) {
            this.state = State.Normal;
        }
    }
}
