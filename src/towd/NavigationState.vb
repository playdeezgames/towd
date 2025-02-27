Friend Class NavigationState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Yer playin' the game!"
            }
        Dim gameMenuButton As New Button With
            {
                .Text = "Menu",
                .Y = Pos.Bottom(titleLabel) + 1
            }
        AddHandler gameMenuButton.Clicked, AddressOf OnGameMenuButtonClicked

        Add(titleLabel, gameMenuButton)
    End Sub

    Private Sub OnGameMenuButtonClicked()
        ShowState(GameState.GameMenu)
    End Sub

    Friend Overrides Sub UpdateView()
    End Sub
End Class
