Imports towd.business
Imports towd.data

Friend Class CraftMenuState
    Inherits ChildView

    Private Const AVAILABLE_TEXT As String = "Available"
    Private Const ALL_TEXT As String = "All"
    Private ReadOnly availableVerbListView As ListView
    Private ReadOnly allVerbListView As ListView
    Private ReadOnly tabView As TabView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Craft (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)

        availableVerbListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler availableVerbListView.OpenSelectedItem, AddressOf OnAvailableVerbListViewOpenSelectedItem

        Dim availableVerbListTab = New TabView.Tab With
            {
                .View = availableVerbListView,
                .Text = AVAILABLE_TEXT
            }

        allVerbListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        allVerbListView.SetSource(VerbTypes.Descriptors.Values.ToList)
        AddHandler allVerbListView.OpenSelectedItem, AddressOf OnAllVerbListViewOpenSelectedItem

        Dim allVerbListTab = New TabView.Tab With
            {
                .View = allVerbListView,
                .Text = ALL_TEXT
            }

        tabView = New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3
            }
        tabView.AddTab(availableVerbListTab, True)
        tabView.AddTab(allVerbListTab, False)
        Add(tabView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(tabView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnAllVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        If descriptor.CanCraft(World.Avatar) Then
            descriptor.Craft(World.Avatar)
            ShowState(GameState.Neutral)
        Else
            MessageBox.ErrorQuery("Sorry Not Sorry!", "You cannot do that.", "OK")
        End If
    End Sub

    Private Sub OnAvailableVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        descriptor.Craft(World.Avatar)
        If Not descriptor.CanCraft(World.Avatar) Then
            World.Avatar.AppendMessage($"You no longer meet the requirements for performing {descriptor.Name}.")
        End If
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim verbs = character.GetDoableVerbs().ToList()
        availableVerbListView.SetSource(verbs)
        Dim lastVerb = character.LastVerb
        Dim verbIndex = If(lastVerb.HasValue, verbs.FindIndex(Function(x) x.VerbType = lastVerb.Value), -1)
        If verbIndex > -1 Then
            availableVerbListView.SelectedItem = verbIndex
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Select Case tabView.SelectedTab.Text
                Case ALL_TEXT
                    TopicState.Topic = VerbTypeTopicTable(CType(allVerbListView.Source.ToList(allVerbListView.SelectedItem), IVerbType).VerbType)
                Case AVAILABLE_TEXT
                    TopicState.Topic = VerbTypeTopicTable(CType(availableVerbListView.Source.ToList(availableVerbListView.SelectedItem), IVerbType).VerbType)
            End Select
            ShowState(GameState.Topic)
        End If
    End Sub

    Private Sub CloseWindow()
        World.Avatar.SetFlag(towd.data.FlagType.CraftMenu, False)
        ShowState(GameState.Neutral)
    End Sub
End Class
