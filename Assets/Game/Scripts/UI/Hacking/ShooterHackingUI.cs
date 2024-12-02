public class ShooterHackingUI : BeginHackingUI
{
    private ShooterLoadingData _hackData;

    protected override void OnConfirmButtonClick()
    {
        MLocator.Instance.HackingUI.Activate();

        MLocator.Instance.HackingManager.SetUpHacking(_hackData);
        base.OnConfirmButtonClick();
    }

    public void SetHackData(ShooterLoadingData hackData)
    {
        _hackData = hackData;
    }
}
