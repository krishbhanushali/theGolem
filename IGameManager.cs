internal interface IGameManager
{
    ManagerStatus status { get; }
    void Startup();
}