using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputKeyHandler : SingletonMonoBehaviour<InputKeyHandler> {

    public event EventHandler OnKeyE;
    public event EventHandler OnKeyEsc;

    private void Awake() {

        base.Awake();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.E)) OnKeyE?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.Escape)) OnKeyEsc?.Invoke(this, EventArgs.Empty);
        
    }
}
