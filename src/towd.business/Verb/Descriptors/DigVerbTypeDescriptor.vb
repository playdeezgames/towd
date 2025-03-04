Imports towd.data

Friend Class DigVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Dig, "Dig", 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AppendMessage("You dig.")
        character.Location.ChangeStatistic(StatisticType.Digging, -1)
        Dim item As IItem
        Dim itemType As IItemType
        If character.Location.EntityType.LocationType = LocationType.Grass Then
            item = character.GetItemsOfType(data.ItemType.SharpStick.ToDescriptor).First
            itemType = data.ItemType.Grub.ToDescriptor
        ElseIf character.Location.EntityType.LocationType = LocationType.Pond Then
            item = character.GetItemsOfType(data.ItemType.SharpStick.ToDescriptor).First
            itemType = data.ItemType.Clay.ToDescriptor
        Else
            Throw New NotImplementedException
        End If
        character.ChangeItemDurability(item, -1)
        character.AddItem(character.World.CreateItem(itemType))
        character.AppendMessage($"+1 {itemType.Name}(x{character.GetCountOfItemType(itemType)})")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If character.GetStatisticSumOfItemType(data.ItemType.SharpStick.ToDescriptor, StatisticType.Durability) <= 0 Then
            Return False
        End If
        Return character.Location.GetStatistic(StatisticType.Digging) > 0
    End Function
End Class
