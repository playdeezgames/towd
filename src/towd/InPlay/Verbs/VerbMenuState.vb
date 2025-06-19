Imports towd.business
Imports towd.data

Friend Class VerbMenuState
    Inherits ChildView
    Private ReadOnly verbListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Verbs (Esc to cancel, F1 for help)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        verbListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3
            }
        AddHandler verbListView.OpenSelectedItem, AddressOf OnVerbListViewOpenSelectedItem
        Add(verbListView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(verbListView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim listItem = CType(args.Value, VerbMenuListViewItem)
        listItem.VerbType.Perform(World.Avatar)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub CloseWindow()
        World.Avatar.SetFlag(FlagType.VerbMenu, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim verbs = VerbTypes.Descriptors.Where(Function(x) character.CanDoVerb(x.Key)).Select(Function(x) New VerbMenuListViewItem(x.Value, character)).OrderBy(Function(x) x.VerbType.Name).ToList
        verbListView.SetSource(verbs)
        Dim lastVerb = character.LastVerb
        If lastVerb.HasValue Then
            Dim verbIndex = verbs.FindIndex(Function(x) x.VerbType.VerbType = lastVerb.Value)
            If verbIndex > -1 Then
                verbListView.SelectedItem = verbIndex
            End If
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Dim currentIndex = verbListView.SelectedItem
            Dim currentItem = verbListView.Source.ToList(currentIndex)
            TopicState.Topic = verbTypeTopicTable(CType(currentItem, VerbMenuListViewItem).VerbType.VerbType)
            ShowState(GameState.Topic)
        End If
    End Sub
End Class
