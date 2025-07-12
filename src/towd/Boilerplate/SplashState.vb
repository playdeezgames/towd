Friend Class SplashState
    Inherits ChildView

    Public Sub New(mainView As MainView, context As IContext)
        MyBase.New(mainView, context)
        Dim titleLabel As New Label With
            {
                .X = Pos.Center,
                .Text = "
.___________.  ______   ____    __    ____  _______  
|           | /  __  \  \   \  /  \  /   / |       \ 
`---|  |----`|  |  |  |  \   \/    \/   /  |  .--.  |
    |  |     |  |  |  |   \            /   |  |  |  |
    |  |     |  `--'  |    \    /\    /    |  '--'  |
    |__|      \______/      \__/  \__/     |_______/ 
"
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
        ShowState(GameState.MainMenu)
    End Sub

    Friend Overrides Sub UpdateView()
        MyBase.UpdateView()
    End Sub
End Class
