Friend Class HatchetItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Hatchet, "Hatchet", False, "A sturdy chopping blade. 
Hew wood or foes with precision. 
Keep it sharp to survive the Wastes.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
        item.SetStatistic(StatisticType.Durability, 30)
        item.SetStatisticMinimum(StatisticType.Durability, 0)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub

    Public Overrides Function Describe(item As IItem) As String
        Return $"{MyBase.Describe(item)}(Durability: {item.GetStatistic(StatisticType.Durability)})"
    End Function
End Class
