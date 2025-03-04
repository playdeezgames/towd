Friend Class EatVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Eat, "Eat")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AppendMessage("You eat.")
        Dim itemType = data.ItemType.CookedGrub.ToDescriptor
        Dim item = character.GetItemsOfType(itemType).First
        Dim satiety = item.GetStatistic(data.StatisticType.Satiety)
        character.RemoveItem(item)
        item.Recycle()
        character.AppendMessage($"-1 {itemType.Name}({character.GetCountOfItemType(itemType)} remaining)")
        character.ChangeStatistic(data.StatisticType.Satiety, satiety)
        character.AppendMessage($"+{satiety} Satiety({character.GetStatistic(data.StatisticType.Satiety)})")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.GetCountOfItemType(data.ItemType.CookedGrub.ToDescriptor) > 0
    End Function
End Class
