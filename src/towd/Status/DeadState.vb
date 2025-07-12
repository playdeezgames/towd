Friend Class DeadState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Text = "Yer dead."
            }
        Dim okButton As New Button With
            {
                .Text = "Ok",
                .Y = Pos.Bottom(titleLabel) + 1
            }
        AddHandler okButton.Clicked, AddressOf OnOkButtonClicked
        Add(titleLabel, okButton)
    End Sub

    Private Sub OnOkButtonClicked()
        World.Abandon()
        ShowState(GameState.MainMenu)
    End Sub

    Friend Overrides Sub UpdateView()
        MyBase.UpdateView()
    End Sub
End Class
