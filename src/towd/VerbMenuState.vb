Imports towd.data

Friend Class VerbMenuState
    Inherits ChildView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim goBackButton As New Button With
            {
                .Text = "Go Back"
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Add(goBackButton)
    End Sub

    Private Sub OnGoBackButtonClicked()
        World.Avatar.SetFlag(FlagType.VerbMenu, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
    End Sub
End Class
