﻿Friend Class FishingNetItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.FishingNet, "Fishing Net", False, "A woven trap for the waters. 
Cast it to catch fish—skill and luck determine your haul.
Also good for making stockings.
Or so I'm told.")
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
