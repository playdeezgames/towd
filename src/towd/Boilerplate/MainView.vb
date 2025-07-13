Friend Class MainView
    Inherits Window
    Private ReadOnly childViews As New Dictionary(Of String, ChildView)
    Private dialogView As DialogView
    Public ReadOnly Context As IUIContext = New UIContext
    Public Sub New()
        MyBase.New()
        dialogView = New DialogView(Me)
        childViews.Add(GameState.Navigation, New NavigationState(Me))
        childViews.Add(GameState.GameMenu, New GameMenuState(Me))
        childViews.Add(GameState.Inventory, New InventoryState(Me))
        childViews.Add(GameState.ItemStack, New ItemStackState(Me))
        childViews.Add(GameState.VerbMenu, New VerbMenuState(Me))
        childViews.Add(GameState.Deeds, New DeedsState(Me))
        childViews.Add(GameState.SkillMenu, New SkillMenuState(Me))
        childViews.Add(GameState.SaveMenu, New SaveMenuState(Me))
        childViews.Add(GameState.Topic, New TopicState(Me))
        childViews.Add(GameState.Map, New MapState(Me))
        childViews.Add(GameState.Statistics, New StatisticsState(Me))
        childViews.Add(GameState.Dialog, New DialogState(Me))

        ShowState(Nothing, New SplashUIDialog(Context))
    End Sub

    Public Sub ShowState(gameState As String, Optional dialog As IUIDialog = Nothing)
        Context.GameState = gameState
        Context.Dialog = dialog
        RemoveAll()
        If Context.Dialog IsNot Nothing Then
            Add(dialogView)
            dialogView.UpdateView()
        ElseIf Context.GameState IsNot Nothing Then
            Add(childViews(Context.GameState))
            childViews(Context.GameState).UpdateView()
        Else
            Application.RequestStop()
        End If
    End Sub
End Class
