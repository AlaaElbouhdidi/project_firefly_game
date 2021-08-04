using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player> {

    private Rigidbody2D _rigidbody2D;
    private float _xInput;
    private float _yInput;
    
    [SerializeField] private float maxMovementSpeed = 10f;
    [SerializeField] private int life;
    private float _currentMovementSpeed;
    
    // Start is called before the first frame update
    void Awake () {
        base.Awake();
        this._currentMovementSpeed = maxMovementSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        PlayerMovementInput();
        PlayerActionInput();
    }
    
    private void FixedUpdate () {
        PlayerMovement();
    }

    private void PlayerMovementInput() {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        if (_yInput != 0 && _xInput != 0) {
            _yInput *= 0.7f;
            _xInput *= 0.7f;
        }
    }

    private void PlayerActionInput() {
        if (Input.GetMouseButtonDown(0)) {
            // todo attack
        }

        if (Input.GetMouseButtonDown(1)) {
            // todo block
        }

        if (Input.GetKey(KeyCode.Space)) {
            // todo dodge
        }
        
        if (Input.GetKey(KeyCode.Escape)) {
            // todo menu    
        }
    }

    private void PlayerMovement() {
        Vector2 moveVec = new Vector2(
            _xInput * _currentMovementSpeed * Time.deltaTime,
            _yInput * _currentMovementSpeed * Time.deltaTime);
        _rigidbody2D.MovePosition(_rigidbody2D.position + moveVec);
    }
}
