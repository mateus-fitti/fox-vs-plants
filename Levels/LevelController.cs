using Godot;
using System;

public class LevelController : Node2D
{
    [Export] public int LevelNumber = 0;
    [Export] public PackedScene NextLevel;
    int totalBushes;
    int bushesCount = 0;

    [Signal]
    public delegate void ObjectiveDestroyed();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        totalBushes = GetNode<YSort>("YSort/Environment").GetChildCount();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void OnObjectiveDestroyed()
    {
        totalBushes--;
        if (totalBushes <= 0)
        {
            ObjectiveComplete();
        }
        GD.Print("MAIS UM OBJETIVO");
    }

    private void ObjectiveComplete()
    {
        var instance = NextLevel.Instance();
        var worldSc = this.GetTree().Root.GetNode<Node2D>("WorldScene");
        worldSc.AddChild(instance);
        QueueFree();
    }
}
