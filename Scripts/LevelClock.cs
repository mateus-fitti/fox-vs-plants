using Godot;
using System;

public class LevelClock : Control
{

    [Export]
    public int _TotalTime = 70;
    Label timeLabel;
    Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timeLabel = GetNode<Label>("TimeLabel");
        timer = GetNode<Timer>("Timer");

        timer.WaitTime = _TotalTime;
        float min = (int)timer.TimeLeft / 60;
        float sec = (int)timer.TimeLeft % 60;
        String minutes = "";
        String seconds = "";
        if (min < 10)
            minutes = "0";
        if (sec < 10)
            seconds = "0";
        minutes += min;
        seconds += sec;
        timeLabel.Text = (minutes + ":" + seconds);
        timer.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        float min = (int)timer.TimeLeft / 60;
        float sec = (int)timer.TimeLeft % 60;
        String minutes = "";
        String seconds = "";
        if (min < 10)
            minutes = "0";
        if (sec < 10)
            seconds = "0";
        minutes += min;
        seconds += sec;
        timeLabel.Text = (minutes + ":" + seconds);
    }
}
