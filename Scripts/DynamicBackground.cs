using Godot;
using System;

public class DynamicBackground : Node2D
{

    [Export]
    public float _WaitTime = 5;
    [Export]
    public int currentColor = -1;

    Sprite bgSprite;
    Texture[] bgColors = { null, null, null, null, null };

    Timer clock;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bgSprite = GetNode<Sprite>("Sprite");
        clock = GetNode<Timer>("Clock");

        bgColors[0] = GD.Load<Texture>("res://.import/GrassBackground_0.png-eb207aa64abe2444e1e2cdc72f2b22f5.stex");
        bgColors[1] = GD.Load<Texture>("res://.import/GrassBackground_1.png-dc547c0e80aeb858ff76d7533d291fc7.stex");
        bgColors[2] = GD.Load<Texture>("res://.import/GrassBackground_2.png-36d6b837cfa2740767fece523589a48d.stex");
        bgColors[3] = GD.Load<Texture>("res://.import/GrassBackground_3.png-442aa660fc703a1d67daaafc48dbd430.stex");
        bgColors[4] = GD.Load<Texture>("res://.import/GrassBackground_4.png-042ea3b2fa249f68e90d4ee59aab8a65.stex");

        if (currentColor < 0)
            currentColor = (int)(GD.Randi() % 5);

        if (_WaitTime > 0)
        {
            clock.Connect("timeout", this, "ChangeColor");
            clock.WaitTime = _WaitTime;
            clock.Start();
        }

        bgSprite.Texture = bgColors[currentColor];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void ChangeColor()
    {
        int newColor = (int)(GD.Randi() % 5);
        if (newColor == currentColor)
        {
            newColor++;
        }
        if (newColor >= bgColors.GetLength(0))
        {
            newColor = 0;
        }
        currentColor = newColor;

        bgSprite.Texture = bgColors[currentColor];
        clock.WaitTime = _WaitTime;
        clock.Start();
    }

}
