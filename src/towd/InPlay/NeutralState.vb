Imports towd.data

Friend Class NeutralState
    Inherits ChildView

    Public Sub New(mainView As MainView, context As IContext)
        MyBase.New(mainView, context)
    End Sub

    Friend Overrides Sub UpdateView()
        SaveGame(SaveSlot.Auto, False)
        Dim character = World.Avatar
        If character.IsDead Then
            ShowState(GameState.Dead)
        Else
            ShowState(GameState.Navigation)
        End If
    End Sub
End Class
