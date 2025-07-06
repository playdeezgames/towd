Imports System.Text
Imports towd.data
Imports towd.business
Imports NStack

Friend Class NavigationState
    Inherits ChildView
    Const MOVE_TEXT = "Move..."
    Const INVENTORY_TEXT = "Inventory..."
    Const VERB_TEXT = "Verb..."
    Const DEEDS_TEXT = "Deeds..."
    Const MENU_TEXT = "Game Menu..."
    Const SKILLS_TEXT = "Skills..."
    Const MAP_TEXT = "Map..."
    Const STATISTICS_TEXT = "Statistics..."
    Private ReadOnly topicTable As IReadOnlyDictionary(Of String, Topic) =
        New Dictionary(Of String, Topic) From
        {
            {DEEDS_TEXT, Topic.NavigationDeeds},
            {MENU_TEXT, Topic.NavigationGameMenu},
            {INVENTORY_TEXT, Topic.NavigationInventory},
            {MOVE_TEXT, Topic.NavigationMove},
            {SKILLS_TEXT, Topic.NavigationSkills},
            {VERB_TEXT, Topic.NavigationVerb},
            {MAP_TEXT, Topic.NavigationMap},
            {STATISTICS_TEXT, Topic.NavigationStatistics}
        }
    Private ReadOnly locationTextView As TextView
    Private ReadOnly characterTextView As TextView
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
        locationTextView = New TextView With
            {
                .Text = "(information about location)",
                .Y = Pos.Bottom(titleLabel),
                .TextAlignment = TextAlignment.Left,
                .Width = [Dim].Percent(75),
                .Height = [Dim].Percent(70),
                .WordWrap = True,
                .Enabled = False
            }
        characterTextView = New TextView With
            {
                .Text = "(information about character)",
                .X = Pos.Right(locationTextView) + 1,
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Percent(70),
                .WordWrap = True,
                .Enabled = False
            }
        commandListView = New ListView With
            {
                .Y = Pos.Bottom(locationTextView),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler commandListView.OpenSelectedItem, AddressOf OnCommandListViewOpenSelectedItem

        Add(
            locationTextView,
            characterTextView,
            commandListView)
    End Sub

    Private Sub OnCommandListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim command = CStr(args.Value)
        Select Case command
            Case MOVE_TEXT
                HandleMoveCommand()
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
            Case MAP_TEXT
                ShowState(GameState.Map)
            Case STATISTICS_TEXT
                ShowState(GameState.Statistics)
        End Select
    End Sub

    Private Sub HandleMoveCommand()
        Dim directionTable = World.Avatar.Location.Neighbors.ToDictionary(Function(x) CType(x.Key.ToDescriptor.Name, ustring), Function(x) x.Key)
        Dim buttons = directionTable.Keys.ToArray
        Dim answer = MessageBox.Query("Which Way?", "Choose a direction.", buttons)
        If answer > -1 Then
            World.Avatar.Move(directionTable(buttons(answer)))
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar

        UpdateLocationTextView(character)

        UpdateCharacterTextView(character)

        UpdateCommandList(character)

        While character.HasMessages
            MessageBox.Query("", String.Join(vbCrLf, character.CurrentMessage), "Ok")
            character.DismissMessage()
        End While
    End Sub

    Private Sub UpdateCommandList(character As business.ICharacter)
        Dim commandList As New List(Of String) From
            {
                MOVE_TEXT,
                MAP_TEXT,
                STATISTICS_TEXT
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

    Private Sub UpdateCharacterTextView(character As business.ICharacter)
        Dim builder As New StringBuilder
        builder.AppendLine($"Statistics:")
        builder.AppendLine($"Satiety: {character.GetStatistic(StatisticType.Satiety)}/{character.GetStatisticMaximum(StatisticType.Satiety)}")
        builder.AppendLine($"Health: {character.GetStatistic(StatisticType.Health)}/{character.GetStatisticMaximum(StatisticType.Health)}")
        builder.AppendLine($"XP: {character.GetStatistic(StatisticType.XP)}")
        characterTextView.Text = builder.ToString
        characterTextView.ColorScheme = New ColorScheme With
                {
                    .Normal = ColorScheme.Normal,
                    .Disabled = ColorScheme.Normal,
                    .Focus = ColorScheme.Focus,
                    .HotFocus = ColorScheme.HotFocus,
                    .HotNormal = ColorScheme.HotNormal
                }
    End Sub

    Private Sub UpdateLocationTextView(character As business.ICharacter)
        Dim location = character.Location
        Dim builder As New StringBuilder
        builder.Append($"{location}")
        Dim neighbors = location.Neighbors
        For Each neighbor In neighbors
            If character.KnowsLocation(neighbor.Value) Then
                builder.AppendLine($"To the {neighbor.Key.ToDescriptor.Name} there is {neighbor.Value.EntityType.Name}.")
            Else
                builder.AppendLine($"To the {neighbor.Key.ToDescriptor.Name} is unexplored.")
            End If
        Next
        locationTextView.Text = builder.ToString
        locationTextView.ColorScheme = New ColorScheme With
                {
                    .Normal = ColorScheme.Normal,
                    .Disabled = ColorScheme.Normal,
                    .Focus = ColorScheme.Focus,
                    .HotFocus = ColorScheme.HotFocus,
                    .HotNormal = ColorScheme.HotNormal
                }
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
