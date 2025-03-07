Imports towd.data

Friend Class NavigationState
    Inherits ChildView
    Const MOVE_TEXT = "Move..."
    Const INVENTORY_TEXT = "Inventory..."
    Const VERB_TEXT = "Verb..."
    Const DEEDS_TEXT = "Deeds..."
    Const MENU_TEXT = "Game Menu..."
    Private ReadOnly locationLabel As Label
    Private ReadOnly characterLabel As Label
    Private ReadOnly commandListView As ListView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        locationLabel = New Label With
            {
                .Text = "(information about location)",
                .TextAlignment = TextAlignment.Left,
                .Width = [Dim].Percent(50),
                .Height = [Dim].Percent(70)
            }
        characterLabel = New Label With
            {
                .Text = "(information about character)",
                .TextAlignment = TextAlignment.Right,
                .X = Pos.Right(locationLabel) + 1,
                .Width = [Dim].Fill
            }
        commandListView = New ListView With
            {
                .Y = Pos.Bottom(locationLabel) + 1,
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler commandListView.OpenSelectedItem, AddressOf OnCommandListViewOpenSelectedItem

        Add(
            locationLabel,
            characterLabel,
            commandListView)
    End Sub

    Private Sub OnCommandListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim command = CStr(args.Value)
        Select Case command
            Case MOVE_TEXT
                World.Avatar.SetFlag(FlagType.MoveMenu, True)
                ShowState(GameState.Neutral)
            Case VERB_TEXT
                World.Avatar.SetFlag(FlagType.VerbMenu, True)
                ShowState(GameState.Neutral)
            Case INVENTORY_TEXT
                World.Avatar.SetFlag(FlagType.Inventory, True)
                ShowState(GameState.Neutral)
            Case DEEDS_TEXT
                ShowState(GameState.Deeds)
            Case MENU_TEXT
                ShowState(GameState.GameMenu)
        End Select
    End Sub
    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim location = character.Location
        locationLabel.Text = $"Location: {location}"
        characterLabel.Text = $"Satiety: {character.GetStatistic(StatisticType.Satiety)}/{character.GetStatisticMaximum(StatisticType.Satiety)}
Health: {character.GetStatistic(StatisticType.Health)}/{character.GetStatisticMaximum(StatisticType.Health)}"
        Dim commandList As New List(Of String) From
            {
                MOVE_TEXT
            }
        If character.HasItems Then
            commandList.Add(INVENTORY_TEXT)
        End If
        If character.CanDoAnyVerb Then
            commandList.Add(VERB_TEXT)
        End If
        commandList.Add(DEEDS_TEXT)
        commandList.Add(MENU_TEXT)
        commandListView.SetSource(commandList)
    End Sub
End Class
