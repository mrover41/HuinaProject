using UnityEngine;

public class PlaerCamera : ModuleBase {
    [SerializeField] private float sensitivity = 5;

    private Transform _cam;
    private Transform _player;

    private float _xRotation = 0;

    public override void OnEnable(Player pl) {
        _cam = Camera.main.transform;
        _player = ScheneManager.Instance.PlayerInstance.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        _cam.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _player.Rotate(0f, mouseX, 0f);        
    }
}