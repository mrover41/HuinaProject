using UnityEngine;
using System;

public class PlayerHealth : ModuleBase {
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public float cureentHealth = 100;

    private EventHandler? _kill;
    public event EventHandler Kill {
        add => _kill += value;
        remove => _kill -= value;
    }

    private void Heal(float h) {
        cureentHealth += Mathf.Clamp(h, 0, maxHealth);
    }

    private void SetHealth(float h) {
        cureentHealth = Mathf.Clamp(h, 0, maxHealth);
    }

    private void Hurt(float h) {
        cureentHealth -= Mathf.Clamp(h, 0, maxHealth);
        if (cureentHealth <= 0) _kill?.Invoke(this, EventArgs.Empty);
    }
}
