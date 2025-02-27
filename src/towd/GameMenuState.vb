Friend Class GameMenuState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Game Menu",
                .X = Pos.Center
            }
        Dim continueButton As New Button With
            {
                .Text = "Continue",
                .X = Pos.Center,
                .Y = Pos.Bottom(titleLabel) + 1
            }
        AddHandler continueButton.Clicked, AddressOf OnContinueButtonClicked
        Dim abandonButton As New Button With
            {
                .Text = "Abandon Game",
                .X = Pos.Center,
                .Y = Pos.Bottom(continueButton) + 1
            }
        AddHandler abandonButton.Clicked, AddressOf OnAbandonButtonClicked
        Add(titleLabel, continueButton, abandonButton)
    End Sub

    Private Sub OnAbandonButtonClicked()
        If MessageBox.Query("Confirm Abandon?", "Are you sure you want to abandon the game?", "Yes", "No") = 0 Then
            World.Abandon()
            ShowState(GameState.MainMenu)
        End If
    End Sub

    Private Sub OnContinueButtonClicked()
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        'nada
    End Sub
End Class
