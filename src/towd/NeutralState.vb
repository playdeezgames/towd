﻿Imports towd.data

Friend Class NeutralState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        If character.HasFlag(FlagType.VerbMenu) Then
            ShowState(GameState.VerbMenu)
        Else
            ShowState(GameState.Navigation)
        End If
    End Sub
End Class
