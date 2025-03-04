Friend Class FishingNetItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.FishingNet, "Fishing Net")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
        item.SetStatistic(data.StatisticType.Durability, 30)
        item.SetStatisticMinimum(data.StatisticType.Durability, 0)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
