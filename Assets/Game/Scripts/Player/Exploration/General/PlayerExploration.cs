public class PlayerExploration : Player
{
    public ExplorationMovement PlayerMovement { get; private set; }

    private void Awake()
    {
        PlayerMovement = GetComponent<ExplorationMovement>();
    }
}
