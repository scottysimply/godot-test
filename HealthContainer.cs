using Godot;
using System;

public partial class HealthContainer : HBoxContainer
{
    int HeartsToDisplay = 3;
    public override void _Ready()
    {
        if (GetParent() is not Player player) return;

        player.OnHealthChange += OnHealthChange;
    }
    private void RemoveAllChildren() {
        foreach (var child in GetChildren()) {
            RemoveChild(child);
            child.QueueFree();
        }
    }
    private void OnHealthChange(int oldHealth, int newHealth) {
        if (oldHealth != newHealth) return;
        
        RemoveAllChildren();
        HeartsToDisplay = newHealth;
        Image fullHeart = new Image();
        fullHeart.Load("res://Assets/UI/Heart.png");
        ImageTexture texture = new ImageTexture();
        texture.SetImage(fullHeart);
        for (int i = 0; i < HeartsToDisplay; i++) {
            TextureRect rect = new()
            {
                Texture = texture
            };
            AddChild(rect);
        }
    }
}
