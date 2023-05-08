using Godot;
using System;

public class MenuButton : TextureButton
{
    GameController gameController;
    AudioStreamPlayer audioPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {  
        audioPlayer = GetNode<AudioStreamPlayer>("SelectSound");
        gameController = GetTree().Root.GetChild(0).GetNode<GameController>(".");
        this.Connect("pressed", this, "PlaySoundButton");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void PlaySoundButton()
    {
        audioPlayer.Play();
    }

    public void OnSoundEnd()
    {
        gameController.GameMenu();
    }
}
