Imports towd.data

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Fish, "Fish")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AppendMessage("You fish.")
        character.Location.ChangeStatistic(StatisticType.Fishing, -1)
        Dim item = character.GetItemsOfType(data.ItemType.FishingNet.ToDescriptor).First
        Dim itemType = data.ItemType.RawFish.ToDescriptor
        character.ChangeItemDurability(item, -1)
        character.AddItem(character.World.CreateItem(itemType))
        character.AppendMessage($"+1 {itemType.Name}(x{character.GetCountOfItemType(itemType)})")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If character.GetStatisticSumOfItemType(data.ItemType.FishingNet.ToDescriptor, StatisticType.Durability) <= 0 Then
            Return False
        End If
        Return character.Location.GetStatistic(StatisticType.Fishing) > 0
    End Function
End Class
