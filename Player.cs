using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public int Health = 3;
    public int MaxHealth = 3;
    public const float Speed = 100.0f;
    public const float JumpVelocity = -200.0f;
    public int JumpTimer = 0;
    public bool Jumping = false;
    public bool JumpHeldDown = false;
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
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }


        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            Jumping = true;
        }
        JumpHeldDown = false;
        if (Input.IsActionPressed("ui_accept")) {
            JumpHeldDown = true;
        }
        if (Jumping) {
            velocity.Y = JumpVelocity;
            if (JumpHeldDown) {
                JumpTimer++;
            }
            // Max jump achieved. Keep physics floaty
            if (JumpTimer > 20) {
                JumpTimer = 0;
                Jumping = false;
                velocity.Y = -100f;
            }
            // Jump was released. Don't make it floaty
            else if (!JumpHeldDown) {
                JumpTimer = 0;
                Jumping = false;
                velocity.Y = -25f;
            }
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
