Friend Class MainView
    Inherits Window
    Private ReadOnly childViews As New Dictionary(Of String, ChildView)
    Private ReadOnly context As IContext = New Context
    Public Sub New()
        MyBase.New()
        childViews.Add(GameState.Splash, New SplashState(Me, context))
        childViews.Add(GameState.MainMenu, New MainMenuState(Me, context))
        childViews.Add(GameState.Neutral, New NeutralState(Me, context))
        childViews.Add(GameState.Navigation, New NavigationState(Me, context))
        childViews.Add(GameState.GameMenu, New GameMenuState(Me, context))
        childViews.Add(GameState.Dead, New DeadState(Me, context))
        childViews.Add(GameState.Inventory, New InventoryState(Me, context))
        childViews.Add(GameState.ItemStack, New ItemStackState(Me, context))
        childViews.Add(GameState.VerbMenu, New VerbMenuState(Me, context))
        childViews.Add(GameState.Deeds, New DeedsState(Me, context))
        childViews.Add(GameState.SkillMenu, New SkillMenuState(Me, context))
        childViews.Add(GameState.SaveMenu, New SaveMenuState(Me, context))
        childViews.Add(GameState.LoadMenu, New LoadMenuState(Me, context))
        childViews.Add(GameState.Topic, New TopicState(Me, context))
        childViews.Add(GameState.Map, New MapState(Me, context))
        childViews.Add(GameState.Statistics, New StatisticsState(Me, context))
        childViews.Add(GameState.Dialog, New DialogState(Me, context))

        ShowState(GameState.Splash)
    End Sub

    Public Sub ShowState(gameState As String)
        RemoveAll()
        Add(childViews(gameState))
        childViews(gameState).UpdateView()
    End Sub
End Class
