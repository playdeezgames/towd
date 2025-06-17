Imports System.Text
Imports towd.data
Imports towd.business

Friend Class NavigationState
    Inherits ChildView
    Const MOVE_TEXT = "Move..."
    Const INVENTORY_TEXT = "Inventory..."
    Const VERB_TEXT = "Verb..."
    Const DEEDS_TEXT = "Deeds..."
    Const MENU_TEXT = "Game Menu..."
    Const SKILLS_TEXT = "Skills..."
    Private ReadOnly topicTable As IReadOnlyDictionary(Of String, Topic) =
        New Dictionary(Of String, Topic) From
        {
            {DEEDS_TEXT, Topic.NavigationDeeds},
            {MENU_TEXT, Topic.NavigationGameMenu},
            {INVENTORY_TEXT, Topic.NavigationInventory},
            {MOVE_TEXT, Topic.NavigationMove},
            {SKILLS_TEXT, Topic.NavigationSkills},
            {VERB_TEXT, Topic.NavigationVerb}
        }
    Private ReadOnly locationLabel As Label
    Private ReadOnly characterLabel As Label
    Private ReadOnly commandListView As ListView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Navigation (F1 for help)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        locationLabel = New Label With
            {
                .Text = "(information about location)",
                .Y = Pos.Bottom(titleLabel) + 1,
                .TextAlignment = TextAlignment.Left,
                .Width = [Dim].Percent(50),
                .Height = [Dim].Percent(70)
            }
        characterLabel = New Label With
            {
                .Text = "(information about character)",
                .TextAlignment = TextAlignment.Right,
                .X = Pos.Right(locationLabel) + 1,
                .Y = Pos.Bottom(titleLabel) + 1,
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
            Case SKILLS_TEXT
                World.Avatar.SetFlag(FlagType.SkillMenu, True)
                ShowState(GameState.Neutral)
        End Select
    End Sub
    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar

        UpdateLocationLabel(character)

        UpdateCharacterLabel(character)

        UpdateCommandList(character)
    End Sub

    Private Sub UpdateCommandList(character As business.ICharacter)
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
        commandList.Add(SKILLS_TEXT)
        commandList.Add(MENU_TEXT)
        commandListView.SetSource(commandList)
    End Sub

    Private Sub UpdateCharacterLabel(character As business.ICharacter)
        Dim builder As New StringBuilder
        builder.AppendLine($"Satiety: {character.GetStatistic(StatisticType.Satiety)}/{character.GetStatisticMaximum(StatisticType.Satiety)}")
        builder.AppendLine($"Health: {character.GetStatistic(StatisticType.Health)}/{character.GetStatisticMaximum(StatisticType.Health)}")
        builder.AppendLine($"XP: {character.GetStatistic(StatisticType.XP)}")
        characterLabel.Text = builder.ToString
    End Sub

    Private Sub UpdateLocationLabel(character As business.ICharacter)
        Dim location = character.Location
        Dim builder As New StringBuilder
        builder.AppendLine($"{location}")

        Dim neighbors = location.Neighbors
        If neighbors.Any Then
            builder.AppendLine()
            builder.AppendLine("Neighbors:")
            For Each neighbor In neighbors
                If character.KnowsLocation(neighbor.Value) Then
                    builder.AppendLine($"{neighbor.Key.ToDescriptor.Name}: {neighbor.Value}")
                Else
                    builder.AppendLine($"{neighbor.Key.ToDescriptor.Name}: ????")
                End If
            Next
        End If

        locationLabel.Text = builder.ToString
    End Sub
    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Dim currentIndex = commandListView.SelectedItem
            Dim currentItem = commandListView.Source.ToList(currentIndex)
            TopicState.Topic = topicTable(CStr(currentItem))
            ShowState(GameState.Topic)
        End If
    End Sub
End Class
