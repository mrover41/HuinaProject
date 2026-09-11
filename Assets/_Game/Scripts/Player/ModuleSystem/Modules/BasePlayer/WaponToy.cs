using UnityEngine;

public class WaponToy : ModuleBase {
    private Transform _cam = Camera.main.transform;

    public override void OnUpdate() {
        if (!Input.GetKey(KeyCode.Q)) return;
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = _cam.position;
        sphere.AddComponent<Rigidbody>().AddForce(_cam.forward);
    }
}