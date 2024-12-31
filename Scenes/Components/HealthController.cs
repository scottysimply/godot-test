using Godot;
using TestProject.Utilities;

namespace TestProject.Scenes.Components
{
    public partial class HealthController : Node
    {
        [ExportGroup("Properties")]
        [Export]
        public int MaxHealth { get; set; } = 5;
        [Export]
        public int Health { get; set; }
        private int _oldHealth;
        public CollisionObject2D Parent { get; set; }
        public delegate void HealthChanged(CollisionObject2D sender, HealthChangeArgs healthChange);
        public event HealthChanged OnHealthChange;
        public override void _Ready()
        {
            Health = _oldHealth = MaxHealth;

            if (GetParent() is CollisionObject2D node) {
                Parent = node;
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            if (_oldHealth != Health) {
                OnHealthChange?.Invoke(Parent, new HealthChangeArgs(_oldHealth, Health));
            }
        }
    }
}