Friend Class MainMenuState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Main Menu",
                .X = Pos.Center
            }

        Dim embarkButton As New Button With
            {
                .Text = "Embark!",
                .Y = Pos.Bottom(titleLabel) + 1,
                .X = Pos.Center,
                .IsDefault = True
            }
        AddHandler embarkButton.Clicked, AddressOf OnEmbarkButtonClicked

        Dim quitButton As New Button With
            {
                .Text = "Quit",
                .Y = Pos.Bottom(embarkButton) + 1,
                .X = Pos.Center
            }
        AddHandler quitButton.Clicked, AddressOf OnQuitButtonClicked

        Add(titleLabel, embarkButton, quitButton)
    End Sub

    Private Sub OnEmbarkButtonClicked()
        World.Initialize()
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnQuitButtonClicked()
        If MessageBox.Query("Confirm Quit", "Are you sure you want to quit?", "No", "Yes") = 1 Then
            Application.RequestStop()
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        'nada
    End Sub
End Class
