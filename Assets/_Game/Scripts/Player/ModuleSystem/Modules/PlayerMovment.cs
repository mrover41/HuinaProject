using UnityEngine;

public class PlaerMovment : ModuleBase {
    private CharacterController _charCnt;

    private float speed = 5f;

    public override void OnEnable(Player pl) {
        _charCnt = pl.gameObject.GetComponent<CharacterController>();
    }

    public override void OnUpdate() {
        _charCnt.Move(player.gameObject.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        _charCnt.Move(player.gameObject.transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}