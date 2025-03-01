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
                .Height = [Dim].Fill - 2
            }
        AddHandler verbListView.OpenSelectedItem, AddressOf OnVerbListViewOpenSelectedItem
        Add(verbListView)
        Dim goBackButton As New Button With
            {
                .Text = "Go Back",
                .Y = Pos.Bottom(verbListView) + 1
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Add(goBackButton)
    End Sub

    Private Sub OnVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        descriptor.Perform(World.Avatar)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnGoBackButtonClicked()
        World.Avatar.SetFlag(FlagType.VerbMenu, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim verbs = VerbTypes.Descriptors.Where(Function(x) character.CanDoVerb(x.Key)).Select(Function(x) x.Value).ToList
        verbListView.SetSource(verbs)
    End Sub
End Class
