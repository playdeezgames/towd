Friend Class NavigationState
    Inherits ChildView
    Private ReadOnly titleLabel As Label

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
            {
                .Text = "Yer playin' the game!"
            }


        Dim northButton As New Button With
            {
                .Text = "N",
                .Y = Pos.Bottom(titleLabel) + 1
            }
        AddHandler northButton.Clicked, AddressOf OnNorthButtonClicked
        Dim eastButton As New Button With
            {
                .Text = "E",
                .Y = Pos.Top(northButton),
                .X = Pos.Right(northButton) + 1
            }
        AddHandler eastButton.Clicked, AddressOf OnEastButtonClicked
        Dim southButton As New Button With
            {
                .Text = "S",
                .Y = Pos.Top(eastButton),
                .X = Pos.Right(eastButton) + 1
            }
        AddHandler southButton.Clicked, AddressOf OnSouthButtonClicked
        Dim westButton As New Button With
            {
                .Text = "W",
                .Y = Pos.Top(southButton),
                .X = Pos.Right(southButton) + 1
            }
        AddHandler westButton.Clicked, AddressOf OnWestButtonClicked

        'TODO: verbs
        'TODO: crafting

        Dim gameMenuButton As New Button With
            {
                .Text = "Menu",
                .Y = Pos.Bottom(northButton) + 1
            }
        AddHandler gameMenuButton.Clicked, AddressOf OnGameMenuButtonClicked

        Add(titleLabel, northButton, eastButton, southButton, westButton, gameMenuButton)
    End Sub

    Private Sub OnSouthButtonClicked()
        World.Avatar.Move(business.Direction.South)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnEastButtonClicked()
        World.Avatar.Move(business.Direction.East)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnWestButtonClicked()
        World.Avatar.Move(business.Direction.West)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnNorthButtonClicked()
        World.Avatar.Move(business.Direction.North)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnGameMenuButtonClicked()
        ShowState(GameState.GameMenu)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim location = character.Location
        titleLabel.Text = $"Character Id: {character.Id}
Character Type: {character.CharacterType.Name}
Location Id: {location.Id}({location.Column},{location.Row})
Location Type: {location.LocationType.Name}"
    End Sub
End Class
