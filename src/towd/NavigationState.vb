Friend Class NavigationState
    Inherits ChildView
    Private ReadOnly titleLabel As Label

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
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
        titleLabel.Text = $"Character Id: {World.Avatar.Id}
Character Type: {World.Avatar.CharacterType.Name}
Location Id: {World.Avatar.Location.Id}
Location Type: {World.Avatar.Location.LocationType.Name}"
    End Sub
End Class
