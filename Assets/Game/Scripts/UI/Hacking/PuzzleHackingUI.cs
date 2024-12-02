public class PuzzleHackingUI : BeginHackingUI
{
    private PuzzleLoadingData _hackData;

    protected override void OnConfirmButtonClick()
    {
        MLocator.Instance.SceneLoader.LoadScene(_hackData.SceneToLoad);
        base.OnConfirmButtonClick();
    }

    public void SetHackData(PuzzleLoadingData hackData)
    {
        _hackData = hackData;
    }
}
