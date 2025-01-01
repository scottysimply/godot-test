using Godot;
using System;

namespace TestProject.Scenes.Components
{
    public partial class InputController : Node
    {
        [ExportGroup("Properties")]
        [Export]
        public float HorizontalSpeed { get; set; } = 100f;
        [Export]
        public float JumpSpeed { get; set; } = 200f;
        [Export]
        public int JumpLength { get; set; } = 20;
        [Export]
        public int CoyoteFrames { get; set; } = 5;
        [Export]
        public int JumpBuffer { get; set; } = 10;
        [Export]
        public bool DoubleJumpEnabled { get; set; } = false;
        int _jumpBufferTimer = 0;
        int _coyoteTimer = 0;
        int _jumpTimer = 0;
        bool _jumping = false;
        bool _jumpHeldDown = false;
        public CharacterBody2D Parent { get; set; }
        public override void _Ready()
        {
            // Grab the instance of the parent node
            if (GetParent() is CharacterBody2D node) {
                Parent = node;
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (Parent is null) return;

            Vector2 velocity = Parent.Velocity;
            // Add the gravity.
            if (!Parent.IsOnFloor())
            {
                velocity += Parent.GetGravity() * (float)delta;
            }
            _coyoteTimer++;
            if (Parent.IsOnFloor()) {
                _coyoteTimer = 0;
            }

            // Handle Jump.
            if (_coyoteTimer <= CoyoteFrames && ((Input.IsActionPressed("ui_accept") && _jumpBufferTimer <= JumpBuffer) || Input.IsActionJustPressed("ui_accept")))
            {
                _jumping = true;
                _jumpBufferTimer += JumpBuffer + 1;
            }
            if (Input.IsActionPressed("ui_accept")) {
                _jumpHeldDown = true;
                _jumpBufferTimer++;
            }
            else {
                _jumpHeldDown = false;
                _jumpBufferTimer = 0;
            }
            if (_jumping) {
                velocity.Y = -1f * JumpSpeed;
                if (_jumpHeldDown) {
                    _jumpTimer++;
                }
                // Max jump achieved. Keep physics floaty
                if (_jumpTimer > JumpLength) {
                    _jumpTimer = 0;
                    _jumping = false;
                    velocity.Y = -0.5f * JumpSpeed;
                }
                // Jump was released. Don't make it floaty
                else if (!_jumpHeldDown) {
                    _jumpTimer = 0;
                    _jumping = false;
                    velocity.Y = -0.25f * JumpSpeed;
                }
            }

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * HorizontalSpeed;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Parent.Velocity.X, 0, HorizontalSpeed);
            }

            Parent.Velocity = velocity;
            Parent.MoveAndSlide();
            if (Parent.IsOnCeiling()) {
                velocity.Y = -0.25f * JumpSpeed;
                _jumping = false;
                _jumpTimer = 0;
                _jumpBufferTimer = 0;
            }
        }
    }
}