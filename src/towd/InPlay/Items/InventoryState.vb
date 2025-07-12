Imports towd.business
Imports towd.data

Friend Class InventoryState
    Inherits ChildView
    Private ReadOnly itemTypeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Inventory (Esc to cancel, F1 for help)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        itemTypeListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
            }
        AddHandler itemTypeListView.OpenSelectedItem, AddressOf OnItemTypeListViewOpenSelectedItem
        Add(itemTypeListView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(itemTypeListView)
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnItemTypeListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim itemStack = CType(args.Value, IItemStack)
        Dim itemType = itemStack.ItemType
        If Not itemType.IsAggregate Then
            ItemStackState.CurrentItemType = itemStack.ItemType
            ShowState(GameState.ItemStack)
        End If
    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.Neutral)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Dim currentIndex = itemTypeListView.SelectedItem
            Dim currentItem = itemTypeListView.Source.ToList(currentIndex)
            TopicState.Topic = Topics.ToItemTypeTopic(CType(currentItem, IItemStack).ItemType.ItemType)
            ShowState(GameState.Topic)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = Context.World.Avatar
        Dim itemStacks = character.ItemStacks.ToList
        itemTypeListView.SetSource(itemStacks)
        MyBase.UpdateView()
    End Sub
End Class
