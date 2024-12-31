using Godot;
using System;

namespace TestProject.Scenes.Objects
{
    public partial class Player : CharacterBody2D
    {
        public int Health = 3;
        public int MaxHealth = 3;
        public delegate void HealthChange(int oldValue, int newValue);
        public event HealthChange OnHealthChange;
        public override void _Ready()
        {
            Health = MaxHealth;
            OnHealthChange?.Invoke(0, MaxHealth);
        }
        public override void _Process(double delta)
        {
            int modifiedHealth = Health;
            // DO UPDATE LOGIC ????

            if (modifiedHealth != Health) {
                OnHealthChange?.Invoke(Health, modifiedHealth);
                Health = modifiedHealth;

            }
        }
    }
}