Friend Class MainMenuState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Main Menu",
                .X = Pos.Center
            }

        Dim quitButton As New Button With
            {
                .Text = "Quit",
                .Y = Pos.Bottom(titleLabel) + 1,
                .X = Pos.Center
            }
        AddHandler quitButton.Clicked, AddressOf OnQuitButtonClicked

        Add(titleLabel, quitButton)
    End Sub

    Private Sub OnQuitButtonClicked()
        If MessageBox.Query("Confirm Quit", "Are you sure you want to quit?", "Yes", "No") = 0 Then
            Application.RequestStop()
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        'nada
    End Sub
End Class
