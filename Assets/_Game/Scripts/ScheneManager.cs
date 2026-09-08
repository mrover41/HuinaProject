using UnityEngine;

public class ScheneManager : MonoBehaviour {
	public static ScheneManager Instance {get; private set;}

	[SerializeField] private Player player;
	public Player PlayerInstance {
		get => player;
		private set => player = value;
	}
	
	void Awake() {
		if (Instance != null) Destroy(this.gameObject);
		Instance = this;
	}

	void OnDisable() {
		Instance = null;
	}
}
