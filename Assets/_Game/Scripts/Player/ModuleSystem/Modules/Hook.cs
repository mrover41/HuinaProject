using UnityEngine;


[System.Serializable]
public class Hook : ModuleBase {
    private bool hooking = false;
    private bool isFinded = false;

    private Vector3 pos;
    private Rigidbody _rb;

    [SerializeField] private float maxLenght = 200;
    [SerializeField] private float speed = 1000;
    [SerializeField] private LayerMask hookLayer = 1 << 6;
    [SerializeField] private float maxDistance = 50;

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.F))
            hooking = true;

        if (Input.GetKeyUp(KeyCode.F)) {
            hooking = false;
            isFinded = false;
        }

        if (hooking) {
            Transform c = Camera.main.gameObject.transform;
            if (!isFinded && Physics.Raycast(c.position, c.forward, out RaycastHit hit, maxLenght, hookLayer)) {
                pos = hit.point;
                isFinded = true;
            }

            _rb.AddForce((pos - c.position).normalized * speed * Time.deltaTime);
        }
    }
}
