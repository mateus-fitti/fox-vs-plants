using Godot;
using System;

public class Bush : Node2D
{

    AnimatedSprite effect;
    Sprite sprite;
    Area2D hurtbox;
    Texture[] bushColors = {null, null, null, null, null};
    String[] bushAnimations = {"white", "green", "purple", "red", "yellow"};
    int currentColor;
    public int currentWorldColor = 0;
    DynamicBackground currentBg;
    Node world;
    AudioStreamPlayer audioPlayerDestroy;
    AudioStreamPlayer audioPlayerHit;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        effect = GetNode<AnimatedSprite>("AnimatedSprite");
        sprite = GetNode<Sprite>("Sprite");
        hurtbox = GetNode<Area2D>("Hurtbox");

        bushColors[0] = GD.Load<Texture>("res://.import/Bush_0.png-039961ba5cb8e8cfa343a77125fe33c3.stex");
        bushColors[1] = GD.Load<Texture>("res://.import/Bush_1.png-64ab243f2ea253bc95ab418bc88ddfec.stex");
        bushColors[2] = GD.Load<Texture>("res://.import/Bush_2.png-b47ffc050f114bdd78a6d1abe1140901.stex");
        bushColors[3] = GD.Load<Texture>("res://.import/Bush_3.png-262c41edc4ec3964db1e9061cf9529c0.stex");
        bushColors[4] = GD.Load<Texture>("res://.import/Bush_4.png-5fa4edbe0e231244ec0b403b2b72c4d9.stex");
        currentColor = (int)(GD.Randi() % 5);
        sprite.Texture = bushColors[currentColor];

        world = GetTree().Root.GetChild(0);
        world.Connect("ObjectiveDestroyed", world, "OnObjectiveDestroyed");

        audioPlayerDestroy = GetNode<AudioStreamPlayer>("AudioDestroy");
        audioPlayerHit = GetNode<AudioStreamPlayer>("AudioHit");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void OnBushAreaEntered(Area2D area)
    {
        if (area.CollisionLayer == 4)
        {
            if (currentColor == currentWorldColor)
            {
                OnBushDestroyed();
            }
        }
        else if (area.CollisionLayer == 64)
        {
            //GD.Print("Achei o background");
            currentBg = area.GetNode<DynamicBackground>("..");
            Sprite bgSprite = area.GetNode<Sprite>("../Sprite");

            bgSprite.Connect("texture_changed", this, "OnBgTextureChanged");
            currentWorldColor = currentBg.currentColor;
        }
        else
        {
            currentColor++;

            if (currentColor >= bushColors.GetLength(0))
            {
                currentColor = 0;
            }
            sprite.Texture = bushColors[currentColor];
            audioPlayerHit.Play();
        }
        
    }

    public void OnBgTextureChanged()
    {
        //GD.Print("COR TROCADA");
        currentWorldColor = currentBg.currentColor;
    }

    // Destruir a bush
    public void OnBushDestroyed()
    {
        sprite.QueueFree();
        hurtbox.QueueFree();
        effect.Visible = true;
        effect.Animation = bushAnimations[currentColor];
        effect.Play();

        audioPlayerDestroy.Play();
    }

    public void OnAnimationFinished()
    {
        world.EmitSignal("ObjectiveDestroyed");
        QueueFree();
    }
}
