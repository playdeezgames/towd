Friend Class NeutralState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
    End Sub

    Friend Overrides Sub UpdateView()
        ShowState(GameState.Navigation)
    End Sub
End Class
