Imports towd.business
Imports towd.data

Friend Class VerbMenuState
    Inherits ChildView
    Private ReadOnly verbListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        verbListView = New ListView With
            {
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
        Dim verbIndex = verbs.FindIndex(Function(x) x.VerbType = If(lastVerb, VerbType.Cancel))
        If verbIndex > -1 Then
            verbListView.SelectedItem = verbIndex
        End If
    End Sub
End Class
