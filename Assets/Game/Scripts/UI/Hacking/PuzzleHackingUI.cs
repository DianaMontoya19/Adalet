using UnityEngine;

public class PuzzleHackingUI : BeginHackingUI
{
    private PuzzleLoadingData _hackData;
    public GameObject[] musicPuzzle;

    protected override void OnConfirmButtonClick()
    {
        //MLocator.Instance.SceneLoader.LoadScene(_hackData.SceneToLoad);
        if(MLocator.Instance.GameManager.HasSolvedSinging == false )
        {
            musicPuzzle[0].SetActive(true);

        }
        else
        {
            musicPuzzle[1].SetActive(true);
            MLocator.Instance.GameManager.SetGameState(GameState.Dancing);
            Debug.Log("confirmo");

        }
        
        base.OnConfirmButtonClick();
    }

    public void SetHackData(PuzzleLoadingData hackData)
    {
        _hackData = hackData;
    }
}
