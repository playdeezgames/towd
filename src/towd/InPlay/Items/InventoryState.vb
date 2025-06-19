Imports towd.business
Imports towd.data

Friend Class InventoryState
    Inherits ChildView
    Private ReadOnly itemTypeListView As ListView
    Private ReadOnly topicTable As IReadOnlyDictionary(Of ItemType, Topic) =
        New Dictionary(Of ItemType, Topic) From
        {
            {ItemType.PlantFiber, Topic.ItemTypePlantFiber},
            {ItemType.Stick, Topic.ItemTypeStick},
            {ItemType.Rock, Topic.ItemTypeRock},
            {ItemType.Twine, Topic.ItemTypeTwine},
            {ItemType.SharpRock, Topic.ItemTypeSharpRock},
            {ItemType.Hatchet, Topic.ItemTypeHatchet},
            {ItemType.Log, Topic.ItemTypeLog},
            {ItemType.Hammer, Topic.ItemTypeHammer},
            {ItemType.Plank, Topic.ItemTypePlank},
            {ItemType.SharpStick, Topic.ItemTypeSharpStick},
            {ItemType.Grub, Topic.ItemTypeGrub},
            {ItemType.CookingFire, Topic.ItemTypeCookingFire},
            {ItemType.CookedGrub, Topic.ItemTypeCookedGrub},
            {ItemType.Clay, Topic.ItemTypeClay},
            {ItemType.Charcoal, Topic.ItemTypeCharcoal},
            {ItemType.UnfiredBrick, Topic.ItemTypeUnfiredBrick},
            {ItemType.Brick, Topic.ItemTypeBrick},
            {ItemType.FishingNet, Topic.ItemTypeFishingNet},
            {ItemType.RawFish, Topic.ItemTypeRawFish},
            {ItemType.RawFishFilet, Topic.ItemTypeRawFishFilet},
            {ItemType.FishHead, Topic.ItemTypeFishHead},
            {ItemType.FishGuts, Topic.ItemTypeFishGuts},
            {ItemType.Knife, Topic.ItemTypeKnife},
            {ItemType.Blade, Topic.ItemTypeBlade},
            {ItemType.CookedFishFilet, Topic.ItemTypeCookedFishFilet},
            {ItemType.Furnace, Topic.ItemTypeFurnace}
        }
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
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Dim currentIndex = itemTypeListView.SelectedItem
            Dim currentItem = itemTypeListView.Source.ToList(currentIndex)
            TopicState.Topic = topicTable(CType(currentItem, IItemStack).ItemType.ItemType)
            ShowState(GameState.Topic)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim itemStacks = character.ItemStacks.ToList
        itemTypeListView.SetSource(itemStacks)
    End Sub
End Class
