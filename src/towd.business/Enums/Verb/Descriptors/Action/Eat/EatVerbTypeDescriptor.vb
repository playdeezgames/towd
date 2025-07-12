Public Class EatVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(verbType As VerbType, verbCategoryType As String, itemType As String)
        MyBase.New(verbType, verbCategoryType, 0)
        Dim itemTypeDescriptor = itemType.ToItemTypeDescriptor
        SetDisplayName($"Eat {itemTypeDescriptor.Name}")
        SetItemTypeInput(itemType, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, itemTypeDescriptor.Statistics(StatisticType.Satiety))
    End Sub
End Class
