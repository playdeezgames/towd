Imports towd.data

Friend Class NavigationState
    Inherits ChildView
    Private ReadOnly titleLabel As Label
    Private ReadOnly verbsButton As Button

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        titleLabel = New Label With
            {
                .Text = "Yer playin' the game!"
            }

        Dim moveLabel = New Label With
            {
                .Text = "Move:",
                .Y = Pos.Bottom(titleLabel) + 1
            }
        Dim northButton As New Button With
            {
                .Text = "N",
                .Y = Pos.Top(moveLabel),
                .X = Pos.Right(moveLabel) + 1
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

        Dim actionsLabel As New Label With
            {
                .Text = "Actions:",
                .Y = Pos.Bottom(moveLabel) + 1
            }
        verbsButton = New Button With
            {
                .Text = "Verbs",
                .Y = Pos.Top(actionsLabel),
                .X = Pos.Right(actionsLabel) + 1
            }
        AddHandler verbsButton.Clicked, AddressOf OnVerbsButtonClicked
        Dim gameMenuButton As New Button With
            {
                .Text = "Menu",
                .Y = Pos.Top(verbsButton),
                .X = Pos.Right(verbsButton) + 1
            }
        AddHandler gameMenuButton.Clicked, AddressOf OnGameMenuButtonClicked

        Add(
            titleLabel,
            moveLabel,
            northButton,
            eastButton,
            southButton,
            westButton,
            actionsLabel,
            verbsButton,
            gameMenuButton)
    End Sub

    Private Sub OnVerbsButtonClicked()
        World.Avatar.SetFlag(FlagType.VerbMenu, True)
        ShowState(GameState.Neutral)
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
        verbsButton.Enabled = character.CanDoAnyVerb
    End Sub
End Class
