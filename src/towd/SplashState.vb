Friend Class SplashState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .X = Pos.Center,
                .Text = "TOWD"
            }
        Dim okButton As New Button With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(titleLabel) + 1,
                .Text = "Ok"
            }
        AddHandler okButton.Clicked, AddressOf OnOkButtonClicked
        Add(titleLabel, okButton)
    End Sub

    Private Sub OnOkButtonClicked()
        mainView.ShowState(GameState.MainMenu)
    End Sub

    Friend Overrides Sub UpdateView()
        'nada
    End Sub
End Class
