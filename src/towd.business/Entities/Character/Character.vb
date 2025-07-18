﻿Imports towd.data

Friend Class Character
    Inherits Entity(Of ICharacterType, CharacterData)
    Implements ICharacter

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData, characterId)
    End Sub
    Public Property CurrentLocation As ILocation Implements ICharacter.CurrentLocation
        Get
            Return New Location(WorldData, EntityData.LocationId)
        End Get
        Set(value As ILocation)
            CurrentLocation.RemoveCharacter(Me)
            EntityData.LocationId = value.Id
            CurrentLocation.AddCharacter(Me)
        End Set
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
            Return EntityData.CharacterType.ToCharacterTypeDescriptor()
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
            Return EntityData.Items.Where(Function(x) x.Value.Count <> 0).Select(Function(x) New ItemStack(Me, x.Key.ToItemTypeDescriptor))
        End Get
    End Property
    Public ReadOnly Property CanDoAnyVerb As Boolean Implements ICharacter.CanDoAnyVerb
        Get
            Return VerbTypes.Descriptors.Any(Function(x) x.Value.CanPerform(Me))
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return EntityData.CharacterType.ToCharacterTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property CanDialog As Boolean Implements ICharacter.CanDialog
        Get
            Return EntityType.CanDialog(Me)
        End Get
    End Property

    Public Sub Move(direction As String) Implements ICharacter.Move
        Dim descriptor = direction.ToDirectionDescriptor
        Dim nextLocation As ILocation = GetNextLocation(descriptor)
        If nextLocation IsNot Nothing Then
            AddKnownLocation(nextLocation)
            AppendMessage($"You move {descriptor.Name}.")
            CurrentLocation = nextLocation
            ChangeStatistic(StatisticType.Steps, 1)
        End If
    End Sub

    Private Function GetNextLocation(descriptor As IDirection) As ILocation
        Dim column = CurrentLocation.Column
        Dim row = CurrentLocation.Row
        Dim nextColumn = descriptor.NextColumn(column, row)
        Dim nextRow = descriptor.NextRow(column, row)
        Dim map = CurrentLocation.Map
        Dim nextLocation = map.GetLocation(nextColumn, nextRow)
        Return nextLocation
    End Function

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

    Public Function GetStatisticSumOfItemType(itemType As IItemType, statisticType As String) As Integer Implements ICharacter.GetStatisticSumOfItemType
        Return GetItemsOfType(itemType).Sum(Function(x) x.GetStatistic(statisticType))
    End Function

    Public Sub RemoveItemOfType(itemType As IItemType) Implements ICharacter.RemoveItemOfType
        Dim itemStack = EntityData.Items(itemType.ItemType)
        itemStack.Remove(itemStack.First)
    End Sub

    Public Sub ChangeItemDurability(item As IItem, delta As Integer) Implements ICharacter.ChangeItemDurability
        item.ChangeStatistic(StatisticType.Durability, delta)
        AppendMessage($"{If(delta > 0, "+", "")}{delta} {item.EntityType.Name} durability({item.GetStatistic(StatisticType.Durability)} remaining)")
        If item.GetStatistic(StatisticType.Durability) <= item.GetStatisticMinimum(StatisticType.Durability) Then
            AppendMessage($"Yer {item.EntityType.Name} broke.")
            RemoveItem(item)
            item.Recycle()
        End If
    End Sub

    Public Sub RemoveItem(item As IItem) Implements ICharacter.RemoveItem
        EntityData.Items(item.EntityType.ItemType).Remove(item.Id)
    End Sub

    Public Function GetDoableVerbs() As IEnumerable(Of IVerbType) Implements ICharacter.GetDoableVerbs
        Return VerbTypes.Descriptors.Values.Where(Function(x) x.CanPerform(Me)).OrderBy(Function(x) x.Name)
    End Function

    Public Function HasDone(deed As IDeed) As Boolean Implements ICharacter.HasDone
        Return EntityData.Deeds.Contains(deed.Deed)
    End Function

    Public Function IsAvailable(deed As IDeed) As Boolean Implements ICharacter.IsAvailable
        If HasDone(deed) Then
            Return False
        End If
        Return deed.IsAvailable(Me)
    End Function

    Public Sub SetDone(descriptor As IDeed) Implements ICharacter.SetDone
        descriptor.Do(Me)
        EntityData.Deeds.Add(descriptor.Deed)
    End Sub

    Public Sub ReportChangeStatistic(statisticType As String, delta As Integer) Implements ICharacter.ReportChangeStatistic
        ChangeStatistic(statisticType, delta)
        If delta > 0 Then
            Me.AppendMessage($"+{delta} {statisticType.ToStatisticTypeDescriptor.Name}({GetStatistic(statisticType)})")
        ElseIf delta < 0 Then
            Me.AppendMessage($"{delta} {statisticType.ToStatisticTypeDescriptor.Name}({GetStatistic(statisticType)})")
        End If
    End Sub

    Public Function CanAdvance(skillType As ISkillType) As Boolean Implements ICharacter.CanAdvance
        Return skillType.CanAdvance(Me)
    End Function

    Public Function CanMove(direction As String) As Boolean Implements ICharacter.CanMove
        Return GetNextLocation(direction.ToDirectionDescriptor) IsNot Nothing
    End Function

    Public Sub AddKnownLocation(location As ILocation) Implements ICharacter.AddKnownLocation
        EntityData.KnownLocations.Add(location.Id)
    End Sub

    Public Function KnowsLocation(location As ILocation) As Boolean Implements ICharacter.KnowsLocation
        Return EntityData.KnownLocations.Contains(location.Id)
    End Function

    Public Function StartDialog(otherCharacter As ICharacter) As IDialog Implements ICharacter.StartDialog
        Return EntityType.StartDialog(Me, otherCharacter)
    End Function
End Class
