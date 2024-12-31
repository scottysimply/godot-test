using Godot;
using System;

namespace TestProject.Scenes.Components
{
    public partial class HealthController : Node
    {
        [ExportGroup("Properties")]
        public int MaxHealth { get; set; } = 5;
        public int Health { get; set; }
        public CollisionObject2D Parent { get; set; }
        public delegate void OnHealthChange(object sender, OnHealthChange healthChange);
        public override void _Ready()
        {
            Health = MaxHealth;
            if (GetParent() is CollisionObject2D node) {
                Parent = node;
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}