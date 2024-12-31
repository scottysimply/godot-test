using Godot;
using TestProject.Utilities;

namespace TestProject.Scenes.Components
{
    public partial class HealthContainer : HBoxContainer
    {
        int HeartsToDisplay = 3;
        public override void _Ready()
        {
            if (GetParent().GetParent() is not HealthController controller) return;
            GD.Print("Added controller");
            controller.OnHealthChange += OnHealthChange;
            OnHealthChange(null, new HealthChangeArgs(0, controller.Health));
        }
        private void RemoveAllChildren() {
            foreach (var child in GetChildren()) {
                RemoveChild(child);
                child.QueueFree();
            }
        }
        private void OnHealthChange(CollisionObject2D sender, HealthChangeArgs args) {
            if (args.OldHealth != args.NewHealth) return;
            
            //RemoveAllChildren();
            HeartsToDisplay = args.NewHealth;
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
}