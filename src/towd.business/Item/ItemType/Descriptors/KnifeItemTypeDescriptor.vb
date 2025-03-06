Friend Class KnifeItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Knife, "Knife", False)
    End Sub

    Public Overrides Sub Initialize(item As IItem)
        item.SetStatistic(data.StatisticType.Durability, 30)
        item.SetStatisticMinimum(data.StatisticType.Durability, 0)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub

    Public Overrides Function Describe(item As IItem) As String
        Return $"{MyBase.Describe(item)}(Durability: {item.GetStatistic(data.StatisticType.Durability)})"
    End Function
End Class
