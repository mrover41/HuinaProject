using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class KillPlayer : ModuleBase {
    [SerializeField] private PlayerHealth _phealth;
    [SerializeField] private int scheneIndx = 0;
    
    public override void OnEnable(Player pl) {
        _phealth = pl.GetModule<PlayerHealth>();
        if (_phealth == null) return;

        _phealth.Kill += OnKilling;
    }

    private void OnKilling(object? sender, EventArgs ev) {
        SceneManager.LoadScene(scheneIndx, LoadSceneMode.Single);
    }
}
