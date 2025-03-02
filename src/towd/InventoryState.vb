Imports towd.business
Imports towd.data

Friend Class InventoryState
    Inherits ChildView
    Private ReadOnly itemTypeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        itemTypeListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 2
            }
        AddHandler itemTypeListView.OpenSelectedItem, AddressOf OnItemTypeListViewOpenSelectedItem
        Add(itemTypeListView)
        Dim goBackButton As New Button With
            {
                .Text = "Go Back",
                .Y = Pos.Bottom(itemTypeListView) + 1
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Add(goBackButton)
    End Sub

    Private Sub OnItemTypeListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        descriptor.Perform(World.Avatar)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnGoBackButtonClicked()
        World.Avatar.SetFlag(FlagType.Inventory, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim itemStacks = character.ItemStacks.ToList
        itemTypeListView.SetSource(itemStacks)
    End Sub
End Class
