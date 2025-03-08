Imports towd.data

Friend Class ChopVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Chop, "Chop", 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AppendMessage("You chop.")
        character.Location.ChangeStatistic(StatisticType.Chopping, -1)
        Dim item = character.GetItemsOfType(data.ItemType.Hatchet.ToDescriptor).First
        Dim itemType = data.ItemType.Log.ToDescriptor
        character.ChangeItemDurability(item, -1)
        character.AddItem(character.World.CreateItem(itemType))
        character.AppendMessage($"+1 {itemType.Name}(x{character.GetCountOfItemType(itemType)})")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If character.GetStatisticSumOfItemType(data.ItemType.Hatchet.ToDescriptor, StatisticType.Durability) <= 0 Then
            Return False
        End If
        Return character.Location.GetStatistic(StatisticType.Chopping) > 0
    End Function
End Class
