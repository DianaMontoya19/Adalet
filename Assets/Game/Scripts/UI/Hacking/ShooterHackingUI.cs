using UnityEngine.Playables;

public class ShooterHackingUI : BeginHackingUI
{
    private ShooterLoadingData _hackData;
    //public PlayableDirector _playableDirector;

    protected override void OnConfirmButtonClick()
    {
        MLocator.Instance.HackingManager.SetUpHacking(_hackData);
        base.OnConfirmButtonClick();
        MLocator.Instance.HackingUI.Activate();
        //_playableDirector.Play();
    }

    public void SetHackData(ShooterLoadingData hackData)
    {
        _hackData = hackData;
    }
}
