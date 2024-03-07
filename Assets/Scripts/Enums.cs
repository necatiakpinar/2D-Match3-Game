namespace NecatiAkpinar.Enums
{
    public enum GameElementType
    {
        None = 0,
        //ColorCubes
        YellowCube = 41256,
        RedCube = 85761,
        BlueCube = 92345,
        GreenCube = 54389,
        PurpleCube = 76912,
        //PowerUps
        RocketVertical = 32911,
        RocketHorizontal = 19413,
        //Tiles
        Playable = 39261,
        Spawner = 55317,
        //Blockers
        BalloonBlocker = 15109,
        //Animals
        Duck = 66210
    }

    public enum ItemFolderType
    {
        ColorCube,
        PowerUp,
        Tile,
        Blocker,
        DroppableAnimal
    }
    public enum ColorCubeType
    {
        YellowCube = GameElementType.YellowCube,
        RedCube = GameElementType.RedCube,
        BlueCube = GameElementType.BlueCube,
        GreenCube = GameElementType.GreenCube,
        PurpleCube = GameElementType.PurpleCube
    }

    public enum PowerUpType
    {
        RocketVertical = GameElementType.RocketVertical,
        RocketHorizontal = GameElementType.RocketHorizontal
    }
    public enum TileType
    {
        Playable = GameElementType.Playable,
        Spawner = GameElementType.Spawner,
    }

    public enum BlockerType
    {
        BalloonBlocker = GameElementType.BalloonBlocker
    }
    public enum AnimalType
    {
        Duck = GameElementType.Duck
    }

    public enum GoalType    
    {
        //ColorCubes
        YellowCube = GameElementType.YellowCube,
        RedCube = GameElementType.RedCube,
        BlueCube = GameElementType.BlueCube,
        GreenCube = GameElementType.GreenCube,
        PurpleCube = GameElementType.PurpleCube,
        //Blockers
        BalloonBlocker = GameElementType.BalloonBlocker,
        //Animals
        Duck = GameElementType.Duck
    }

    public enum TileStateType
    {
        Idle,
        Activation,
        Empty
    }
    public enum GameStateType
    {
        None,
        Starting,
        InGame,
        LevelEnd
    }
    public enum PhaseStateType
    {
        None,
        Input,
        Decision,
        Fill,
        Idle
    }

    public enum TileDirectionType
    {
        Up,
        Right,
        Down,
        Left
    }

    public enum SFXGroupType
    {
        General,
        Match2,
        Music
    }
}