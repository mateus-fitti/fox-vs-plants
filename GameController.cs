using Godot;
using System;

public class GameController : Node2D
{
    PackedScene menuScene;
    PackedScene victoryScene;
    PackedScene gameOverScene;
    PackedScene loadLevelScene;
    PackedScene[] levelScenes = {null, null, null};
    Node currentScene;
    public int currentLevel;
    int totalBushes = 0;
    int bushesCount = 0;
    Label levelLabel;

    [Signal]
    public delegate void ObjectiveDestroyed();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        menuScene = GD.Load<PackedScene>("res://UI/Menu.tscn");
        levelScenes[0] = GD.Load<PackedScene>("res://Levels/World1.tscn");
        levelScenes[1] = GD.Load<PackedScene>("res://Levels/World2.tscn");
        levelScenes[2] = GD.Load<PackedScene>("res://Levels/World3.tscn");
        victoryScene = GD.Load<PackedScene>("res://UI/EndScreen.tscn");
        gameOverScene = GD.Load<PackedScene>("res://UI/GameOverScreen.tscn");

        GameStart();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void OnObjectiveDestroyed()
    {
        bushesCount++;

        levelLabel.Text = (bushesCount + " / " + totalBushes);

        if (bushesCount >= totalBushes)
            LoadLevel();
    }

    void LoadScene(PackedScene sc)
    {
        currentScene.QueueFree();
        var instance = sc.Instance();
        AddChild(instance);

        currentScene = GetChild(2);
    }

    void GameStart()
    {
        var instance = menuScene.Instance();
        AddChild(instance);

        currentScene = GetChild(1);
        currentLevel = 0;
    }

    public void GameMenu()
    {
        LoadScene(menuScene);
        currentLevel = 0;
    }

    public void LoadLevel()
    {
        if (currentLevel >= levelScenes.Length)
        {
            currentLevel = 0;
            LoadScene(victoryScene);
        }
        else
        {
            LoadScene(levelScenes[currentLevel]);
            GetLevelData(currentScene);
            currentLevel++;
        }
    }

    void GetLevelData(Node level)
    {
        GD.Print(level.Name);
        totalBushes = level.GetNode<YSort>("YSort/Environment").GetChildCount();
        bushesCount = 0;

        Label levelNumber = level.GetNode<Label>("CanvasLayer/LevelUI/LevelNumber");
        levelNumber.Text = "Level " + (currentLevel+1);

        Timer levelTimer = level.GetNode<Timer>("CanvasLayer/LevelUI/LevelClock/Timer");
        levelTimer.Connect("timeout", this, "GameOver");

        levelLabel = level.GetNode<Label>("CanvasLayer/LevelUI/LevelBushes/BushLabel");

        levelLabel.Text = (bushesCount + " / " + totalBushes);
    }

    public void GameOver()
    {
        currentLevel--;

        LoadScene(gameOverScene);
    }

}
