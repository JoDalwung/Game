public class GameManager : IScene
{
    public IScene.SceneName EntryScene;
    protected override void _OnLoad()
    {
        base._OnLoad();
        DontDestroyOnLoad(gameObject);
    }
    protected override void _OnLoadComplete()
    {
        base._OnLoadComplete();
#if  !UNITY_WEBGL || UNITY_EDITOR      
        _StartLogin();
#endif
        ActiveScene(EntryScene , false);
    }
    protected override void _OnEnter() => _AddEvent();
    protected override void _OnExite() => _RemoveEvent();
    void _AddEvent()
    {
        LobbyDialog.LobbyDialog_GameStartBtn_act += LobbyDialog_LobbyDialog_GameStartBtn_act;
        MainGameDialog.LeaveScene_act += MainGameDialog_LeaveMainGameScene_act;
    }
    void _RemoveEvent()
    {
        LobbyDialog.LobbyDialog_GameStartBtn_act -= LobbyDialog_LobbyDialog_GameStartBtn_act;
        MainGameDialog.LeaveScene_act -= MainGameDialog_LeaveMainGameScene_act;
    }

#if !UNITY_WEBGL || UNITY_EDITOR
    void _StartLogin() => PlayerInfo.Instance.LoadScore();
#endif

    private void LobbyDialog_LobbyDialog_GameStartBtn_act() => ActiveScene(SceneName.MainGameScene, true);
    private void MainGameDialog_LeaveMainGameScene_act() => ActiveScene(SceneName.LobbyScene, false);
}
