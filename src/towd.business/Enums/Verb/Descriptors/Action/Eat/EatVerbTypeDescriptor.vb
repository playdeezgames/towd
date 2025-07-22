Public Class EatVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(
                  verbType As String,
                  verbCategoryType As String,
                  itemType As String,
                  flavorText As String)
        MyBase.New(verbType, verbCategoryType, 0)
        Dim itemTypeDescriptor = itemType.ToItemTypeDescriptor
        SetDisplayName($"Eat {itemTypeDescriptor.Name}")
        SetItemTypeInput(itemType, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, itemTypeDescriptor.Statistics(StatisticType.Satiety))
        SetFlavorText(flavorText)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
