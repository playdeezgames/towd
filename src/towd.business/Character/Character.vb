Imports towd.data

Friend Class Character
    Inherits Entity(Of ICharacterType, CharacterData)
    Implements ICharacter

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData, characterId)
    End Sub
    Public Function CanDoVerb(verbType As VerbType) As Boolean Implements ICharacter.CanDoVerb
        Return verbType.ToDescriptor.CanPerform(Me)
    End Function


    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(WorldData, EntityData.LocationId)
        End Get
        Set(value As ILocation)
            EntityData.LocationId = value.Id
        End Set
    End Property

    Public ReadOnly Property CanDoAnyVerb As Boolean Implements ICharacter.CanDoAnyVerb
        Get
            Return VerbTypes.Descriptors.Keys.Any(Function(x) CanDoVerb(x))
        End Get
    End Property

    Public ReadOnly Property IsAvatar As Boolean Implements ICharacter.IsAvatar
        Get
            Return WorldData.AvatarId.HasValue AndAlso Id = WorldData.AvatarId.Value
        End Get
    End Property

    Public ReadOnly Property HasMessages As Boolean Implements ICharacter.HasMessages
        Get
            Return IsAvatar AndAlso WorldData.Messages.Count <> 0
        End Get
    End Property

    Public ReadOnly Property CurrentMessage As String() Implements ICharacter.CurrentMessage
        Get
            If HasMessages Then
                Return WorldData.Messages(0).ToArray
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Overrides Property EntityType As ICharacterType
        Get
            Return EntityData.CharacterType.ToDescriptor()
        End Get
        Set(value As ICharacterType)
            EntityData.CharacterType = value.CharacterType
            value.Initialize(Me)
        End Set
    End Property

    Protected Overrides ReadOnly Property EntityData As CharacterData
        Get
            Return WorldData.Characters(Id)
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return GetStatistic(StatisticType.Health) <= GetStatisticMinimum(StatisticType.Health)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements ICharacter.HasItems
        Get
            Return EntityData.Items.Any(Function(x) x.Value.Count <> 0)
        End Get
    End Property

    Public ReadOnly Property ItemStacks As IEnumerable(Of IItemStack) Implements ICharacter.ItemStacks
        Get
            Return EntityData.Items.Where(Function(x) x.Value.Count <> 0).Select(Function(x) New ItemStack(Me, x.Key.ToDescriptor))
        End Get
    End Property

    Public Property LastVerb As VerbType? Implements ICharacter.LastVerb
        Get
            If HasStatistic(StatisticType.LastVerb) Then
                Return CType(GetStatistic(StatisticType.LastVerb), VerbType)
            Else
                Return Nothing
            End If
        End Get
        Set(value As VerbType?)
            If value.HasValue Then
                SetStatistic(StatisticType.LastVerb, CInt(value.Value))
            Else
                ClearStatistic(StatisticType.LastVerb)
            End If
        End Set
    End Property

    Public Property LastRecipe As RecipeType? Implements ICharacter.LastRecipe
        Get
            If HasStatistic(StatisticType.LastRecipe) Then
                Return CType(GetStatistic(StatisticType.LastRecipe), RecipeType)
            Else
                Return Nothing
            End If
        End Get
        Set(value As RecipeType?)
            If value.HasValue Then
                SetStatistic(StatisticType.LastRecipe, CInt(value.Value))
            Else
                ClearStatistic(StatisticType.LastRecipe)
            End If
        End Set
    End Property

    Public Property CurrentItemType As IItemType Implements ICharacter.CurrentItemType
        Get
            If HasStatistic(StatisticType.CurrentItemType) Then
                Return CType(GetStatistic(StatisticType.CurrentItemType), data.ItemType).ToDescriptor
            Else
                Return Nothing
            End If
        End Get
        Set(value As IItemType)
            If value IsNot Nothing Then
                SetStatistic(StatisticType.CurrentItemType, CInt(value.ItemType))
            Else
                ClearStatistic(StatisticType.CurrentItemType)
            End If
        End Set
    End Property

    Public Sub Move(direction As Direction) Implements ICharacter.Move
        Dim descriptor = direction.ToDescriptor
        Dim column = Location.Column
        Dim row = Location.Row
        Dim nextColumn = descriptor.NextColumn(column, row)
        Dim nextRow = descriptor.NextRow(column, row)
        Dim map = Location.Map
        Dim nextLocation = map.GetLocation(nextColumn, nextRow)
        If nextLocation IsNot Nothing Then
            Location = nextLocation
        End If
    End Sub

    Public Sub AddMessage(ParamArray lines() As String) Implements ICharacter.AddMessage
        If IsAvatar Then
            WorldData.Messages.Add(lines.ToList)
        End If
    End Sub

    Public Sub DismissMessage() Implements ICharacter.DismissMessage
        If HasMessages Then
            WorldData.Messages.RemoveAt(0)
        End If
    End Sub

    Public Overrides Sub AdvanceTime(amount As Integer)
        EntityType.AdvanceTime(Me, amount)
    End Sub

    Public Sub AppendMessage(ParamArray lines() As String) Implements ICharacter.AppendMessage
        If IsAvatar Then
            If HasMessages Then
                WorldData.Messages.Last.AddRange(lines)
            Else
                AddMessage(lines)
            End If
        End If

    End Sub
    Public Sub AddItem(item As IItem) Implements ICharacter.AddItem
        Dim itemType = item.EntityType.ItemType
        Dim value As HashSet(Of Integer) = Nothing
        If Not EntityData.Items.TryGetValue(itemType, value) Then
            value = New HashSet(Of Integer)
            EntityData.Items.Add(itemType, value)
        End If
        value.Add(item.Id)
    End Sub

    Public Function GetCountOfItemType(itemType As IItemType) As Integer Implements ICharacter.GetCountOfItemType
        Dim value As HashSet(Of Integer) = Nothing
        If EntityData.Items.TryGetValue(itemType.ItemType, value) Then
            Return value.Count
        End If
        Return 0
    End Function

    Public Function GetItemsOfType(ItemType As IItemType) As IEnumerable(Of IItem) Implements ICharacter.GetItemsOfType
        Dim value As HashSet(Of Integer) = Nothing
        If EntityData.Items.TryGetValue(ItemType.ItemType, value) Then
            Return value.Select(Function(x) New Item(WorldData, x))
        End If
        Return Array.Empty(Of IItem)
    End Function

    Public Function GetStatisticSumOfItemType(itemType As IItemType, statisticType As StatisticType) As Integer Implements ICharacter.GetStatisticSumOfItemType
        Return GetItemsOfType(itemType).Sum(Function(x) x.GetStatistic(statisticType))
    End Function

    Public Sub RemoveItemOfType(itemType As IItemType) Implements ICharacter.RemoveItemOfType
        Dim itemStack = EntityData.Items(itemType.ItemType)
        itemStack.Remove(itemStack.First)
    End Sub

    Public Sub ChangeItemDurability(item As IItem, delta As Integer) Implements ICharacter.ChangeItemDurability
        item.ChangeStatistic(StatisticType.Durability, delta)
        If item.GetStatistic(StatisticType.Durability) <= item.GetStatisticMinimum(StatisticType.Durability) Then
            RemoveItem(item)
            item.Recycle()
        End If
    End Sub

    Public Sub RemoveItem(item As IItem) Implements ICharacter.RemoveItem
        EntityData.Items(item.EntityType.ItemType).Remove(item.Id)
    End Sub

    Public Function GetCraftableRecipes() As IEnumerable(Of IRecipeType) Implements ICharacter.GetCraftableRecipes
        Return RecipeTypes.Descriptors.Values.Where(Function(x) x.CanCraft(Me)).OrderBy(Function(x) x.Name)
    End Function
End Class
