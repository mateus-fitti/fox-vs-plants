using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int Speed = 200;
    [Export] public int Boost = 1;

    Vector2 direction = new Vector2();

    AnimationTree animTree = new AnimationTree();
    AnimationNodeStateMachinePlayback animState;

    PackedScene projectile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animTree = GetNode<AnimationTree>("AnimationTree");
        animState = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");
        projectile = GD.Load<PackedScene>("res://Player/LeafProjectile.tscn");
		GD.Print("PRONTO!");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        MoveInput();
        AttackInput();
    }

    // PARA TESTES
    // Método para teleportar o jogador para a posição do mouse
    //
    // public override void _Input(InputEvent @event)
    // {
    //     if (@event.IsActionPressed("click"))
    //     {
    //         //velocity = MoveAndSlide(GetGlobalMousePosition());

    //         GlobalTranslate(-GlobalPosition);
    //         GlobalTranslate(GetGlobalMousePosition());

    //         GD.Print("MOUSE CLICKED");
    //     }
    // }

    private void MoveInput()
    {
        Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed("right"))
            velocity.x += 1;
        if (Input.IsActionPressed("left"))
            velocity.x -= 1;
        if (Input.IsActionPressed("up"))
            velocity.y -= 1;
        if (Input.IsActionPressed("down"))
            velocity.y += 1;

        // if (Input.IsActionPressed("run"))
        //     boostVal = Boost;

        if (velocity != Vector2.Zero)
        {
            animTree.Set("parameters/Idle/blend_position", velocity);
            animTree.Set("parameters/Run/blend_position", velocity);

            velocity = velocity.Normalized();
            direction = velocity;

            animState.Travel("Run");
        }
        else
        {
            animState.Travel("Idle");
        }

        MoveAndSlide(velocity * Speed * Boost);

    }

    private void AttackInput()
    {
        if (Input.IsActionJustPressed("attack"))
        {
            animTree.Set("parameters/Attack/blend_position", GlobalPosition.DirectionTo(GetGlobalMousePosition()));
            animState.Travel("Attack");
        }

        if (Input.IsActionJustPressed("attack2"))
        {
            var instance = projectile.Instance();
            //this.GetTree().CurrentScene.AddChild(instance);
            this.GetParent().AddChild(instance);
            Node2D leaf = (Node2D)instance;
            leaf.GlobalPosition = GlobalPosition;

            // Atira onde o personagem está olhando
            //float leafRotation = direction.Angle();
            float leafRotation = GlobalPosition.DirectionTo(GetGlobalMousePosition()).Angle();
            leaf.Rotation = leafRotation;
        }

    }
}
