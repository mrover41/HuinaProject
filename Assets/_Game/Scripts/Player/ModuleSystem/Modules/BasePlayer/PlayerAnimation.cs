using UnityEngine;

public class PlayerAnimation : ModuleBase {
    private Animator _anim = null;
    private PlayerMovment _playerMov = null;

    private bool isIdle = true;

    public override void OnEnable(Player pl) {
        _anim = pl.GetComponent<Animator>();
        _playerMov = pl.GetModule<PlayerMovment>();
    }

    public override void OnUpdate() {
        if (isIdle && _playerMov.isWalking) {
            _anim.SetTrigger("ToWalking");
            isIdle = false;
        } else if (!isIdle && !_playerMov.isWalking) {
            _anim.SetTrigger("ToIdle");
            isIdle = true;
        }
    }

    public override void OnDisable() {
        _anim = null;
        _playerMov = null;        
    }
}
