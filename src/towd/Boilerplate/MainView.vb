Friend Class MainView
    Inherits Window
    Private ReadOnly childViews As New Dictionary(Of String, ChildView)
    Private ReadOnly context As IContext = New Context
    Public Sub New()
        MyBase.New()
        childViews.Add(GameState.Splash, New SplashState(Me))
        childViews.Add(GameState.MainMenu, New MainMenuState(Me))
        childViews.Add(GameState.Neutral, New NeutralState(Me))
        childViews.Add(GameState.Navigation, New NavigationState(Me))
        childViews.Add(GameState.GameMenu, New GameMenuState(Me))
        childViews.Add(GameState.Dead, New DeadState(Me))
        childViews.Add(GameState.Inventory, New InventoryState(Me))
        childViews.Add(GameState.ItemStack, New ItemStackState(Me))
        childViews.Add(GameState.VerbMenu, New VerbMenuState(Me))
        childViews.Add(GameState.Deeds, New DeedsState(Me))
        childViews.Add(GameState.SkillMenu, New SkillMenuState(Me))
        childViews.Add(GameState.SaveMenu, New SaveMenuState(Me))
        childViews.Add(GameState.LoadMenu, New LoadMenuState(Me))
        childViews.Add(GameState.Topic, New TopicState(Me))
        childViews.Add(GameState.Map, New MapState(Me))
        childViews.Add(GameState.Statistics, New StatisticsState(Me))
        childViews.Add(GameState.Dialog, New DialogState(Me))

        ShowState(GameState.Splash)
    End Sub

    Public Sub ShowState(gameState As String)
        RemoveAll()
        Add(childViews(gameState))
        childViews(gameState).UpdateView()
    End Sub
End Class
