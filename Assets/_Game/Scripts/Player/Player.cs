using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] public ModuleBase[] modules;
	
	private delegate void UpdateDel();
	private delegate void FixedUpdateDel();

	private UpdateDel updateDel;
	private FixedUpdateDel fixedUpdateDel;

	void Start() {
		modules = new ModuleBase[] { new PlaerMovment(), new PlaerCamera(), new WaponToy() };

		foreach (ModuleBase module in modules) {
			module.EnableModule(this);
			updateDel += module.OnUpdate;
			fixedUpdateDel += module.OnFixedUpdate;
		}	
	}

	void Update() {
		updateDel();
	}

	void FixedUpdate() {
		fixedUpdateDel();
	}

	void OnDisable() {
		foreach (ModuleBase module in modules) {
			module.DisableModule();
		}
	}
}