using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerStaminaSystem : SingletonMonoBehaviour<PlayerStaminaSystem> {
    
    private float _maxStamina = 100;
    private float _currentStamina;
    private float _currentStaminaRefreshRate;
    private float tmpStaminaCoast;
    private bool _blocking = false;
    
    [SerializeField] private float maxStaminaRefreshRate = 20f;
    [SerializeField] private Slider staminaBar;

    new void Awake() {
        base.Awake();
        this._currentStaminaRefreshRate = maxStaminaRefreshRate;
        this._currentStamina = _maxStamina;
    }    
    
    void Update() {
        RefreshStamina();
        UpdateStaminaBar();
    }

    private void RefreshStamina() {
        if (_currentStamina < _maxStamina && !_blocking) {
           IncreaseStamina(_currentStaminaRefreshRate * Time.deltaTime);
        } else if (_blocking) {
            ReduceStamina(tmpStaminaCoast * Time.deltaTime);
        }
    }

    private void UpdateStaminaBar() {
        staminaBar.value = _currentStamina;
    }

    public bool ReduceStamina(float amount) {
        if (_currentStamina - amount < 0) {
            return false;
        }
        this._currentStamina -= amount;
        return true;
    }

    public void IncreaseStamina(float amount) {
        this._currentStamina += amount;
        if (_currentStamina > _maxStamina) {
            _currentStamina = _maxStamina;
        }
    }

    public bool ActivateShieldBlock(float staminaCoast) {
        this._blocking = true;
        this.tmpStaminaCoast = staminaCoast;
        return ReduceStamina(staminaCoast * Time.deltaTime);
    }

    public void DeactivateShieldBlock() {
        this._blocking = false;
    }

    public bool CheckForStaminaCoast(float amount) {         
        if (_currentStamina - amount < 1) {
            return false;
        }
        return true;
    }
}
