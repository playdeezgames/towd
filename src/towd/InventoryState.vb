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
                .Height = [Dim].Fill
            }
        AddHandler itemTypeListView.OpenSelectedItem, AddressOf OnItemTypeListViewOpenSelectedItem
        Add(itemTypeListView)
    End Sub

    Private Sub OnItemTypeListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim itemStack = CType(args.Value, IItemStack)
        Dim itemType = itemStack.ItemType
        If Not itemType.IsAggregate Then
            World.Avatar.CurrentItemType = itemStack.ItemType
            ShowState(GameState.Neutral)
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            World.Avatar.SetFlag(FlagType.Inventory, False)
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim itemStacks = character.ItemStacks.ToList
        itemTypeListView.SetSource(itemStacks)
    End Sub
End Class
