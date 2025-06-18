Friend MustInherit Class EatVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly itemType As IItemType

    Protected Sub New(verbType As VerbType, name As String, timeTaken As Integer, itemType As IItemType, description As String)
        MyBase.New(verbType, name, timeTaken, description)
        Me.itemType = itemType
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AppendMessage($"You eat {itemType.Name}.")
        Dim item = character.GetItemsOfType(itemType).First
        Dim satiety = item.GetStatistic(data.StatisticType.Satiety)
        character.RemoveItem(item)
        item.Recycle()
        character.AppendMessage($"-1 {itemType.Name}({character.GetCountOfItemType(itemType)} remaining)")
        character.ChangeStatistic(data.StatisticType.Satiety, satiety)
        character.AppendMessage($"+{satiety} Satiety({character.GetStatistic(data.StatisticType.Satiety)})")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.GetCountOfItemType(itemType) > 0
    End Function
End Class
