Friend Class MainView
    Inherits Window
    Private ReadOnly childViews As New Dictionary(Of GameState, ChildView)
    Public Sub New()
        MyBase.New()
        childViews.Add(GameState.Splash, New SplashState(Me))
        childViews.Add(GameState.MainMenu, New MainMenuState(Me))
        childViews.Add(GameState.Neutral, New NeutralState(Me))
        childViews.Add(GameState.Navigation, New NavigationState(Me))
        childViews.Add(GameState.GameMenu, New GameMenuState(Me))
        childViews.Add(GameState.VerbMenu, New VerbMenuState(Me))
        childViews.Add(GameState.Message, New MessageState(Me))
        childViews.Add(GameState.Dead, New DeadState(Me))
        childViews.Add(GameState.Inventory, New InventoryState(Me))
        ShowState(GameState.Splash)
    End Sub
    Public Sub ShowState(gameState As GameState)
        RemoveAll()
        Add(childViews(gameState))
        childViews(gameState).UpdateView()
    End Sub
End Class
