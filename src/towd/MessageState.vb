Friend Class MessageState
    Inherits ChildView
    Private ReadOnly messageLabel As Label

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        messageLabel = New Label With
            {
                .Text = "yer message here!"
            }
        Dim okButton As New Button With
            {
                .Text = "Ok",
                .Y = Pos.Bottom(messageLabel) + 1
            }
        AddHandler okButton.Clicked, AddressOf OnOkButtonClicked
        Add(messageLabel, okButton)
    End Sub

    Private Sub OnOkButtonClicked()
        World.Avatar.DismissMessage()
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        messageLabel.Text = String.Join("
", World.Avatar.CurrentMessage)
    End Sub
End Class
