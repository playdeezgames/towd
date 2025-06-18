Imports towd.data

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Fish, "Fish", 1, "Cast a line into murky waters. 
Reel in a meal if your skill holds—poor attempts might snag boots or worse. 
Patience pays off, or you dine on disappointment.")
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

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.Location.GetStatistic(StatisticType.Fishing)
    End Function
End Class
