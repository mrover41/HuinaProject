using UnityEngine;

[System.Serializable]
public abstract class ModuleBase {
    public virtual string Name { get; }
    public bool IsEnabled { get; internal set; } = false;
    public Player player {get; private set;}

    protected ModuleBase() {
        if(Name == null)
            Name = GetType().Name;
    }

    internal void EnableModule(Player player) {
        this.player = player;
        IsEnabled = true;
        OnEnable(player);
    }

    internal void DisableModule() {
        IsEnabled = false;
        OnDisable();
    }

    public virtual void OnEnable(Player pl) => Debug.Log($"Module {Name} enabled.");
    public virtual void OnDisable() => Debug.Log($"Module {Name} disabled.");
    public virtual void OnUpdate() {}
    public virtual void OnFixedUpdate() {}
    public virtual void OnCollisionEnter(Collision _) {}
    public virtual void OnCollisionExit(Collision _) {}
    public virtual void OnTriggerEnter(Collider _) {}
    public virtual void OnTriggerExit(Collider _) {}
}