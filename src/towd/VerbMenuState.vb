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
                .Text = "Verbs (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        verbListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler verbListView.OpenSelectedItem, AddressOf OnVerbListViewOpenSelectedItem
        Add(verbListView)
    End Sub

    Private Sub OnVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        descriptor.Perform(World.Avatar)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim verbs = VerbTypes.Descriptors.Where(Function(x) character.CanDoVerb(x.Key)).Select(Function(x) x.Value).OrderBy(Function(x) x.Name).ToList
        verbListView.SetSource(verbs)
        Dim lastVerb = character.LastVerb
        If lastVerb.HasValue Then
            Dim verbIndex = verbs.FindIndex(Function(x) x.VerbType = lastVerb.Value)
            If verbIndex > -1 Then
                verbListView.SelectedItem = verbIndex
            End If
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            World.Avatar.SetFlag(FlagType.VerbMenu, False)
            ShowState(GameState.Neutral)
        End If
    End Sub
End Class
