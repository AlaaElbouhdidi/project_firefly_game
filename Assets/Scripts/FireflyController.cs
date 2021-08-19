using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireflyController : SingletonMonoBehaviour<FireflyController> {

    private Rigidbody2D _rigidbody2D;
    
    public float distance = 2;
    public float moveSpeed = 2f;
    private bool attachedToPlayer = true;
    private Vector3 _moveDirection;
    
    // Start is called before the first frame update
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if (attachedToPlayer) {
            _rigidbody2D.transform.position = Player.Instance.transform.position - new Vector3(0.5f, - 0.5f, 0);
        }
    }
}
