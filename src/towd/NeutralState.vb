Imports towd.data

Friend Class NeutralState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        If character.HasMessages Then
            ShowState(GameState.Message)
        ElseIf character.IsDead Then
            ShowState(GameState.Dead)
        ElseIf character.HasFlag(FlagType.Inventory) Then
            ShowState(GameState.Inventory)
        ElseIf character.HasFlag(FlagType.VerbMenu) Then
            ShowState(GameState.VerbMenu)
        Else
            ShowState(GameState.Navigation)
        End If
    End Sub
End Class
