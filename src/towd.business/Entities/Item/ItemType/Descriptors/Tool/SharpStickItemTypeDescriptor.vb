﻿Friend Class SharpStickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.SharpStick, "Sharp Stick", False, "A pointed branch. 
Jab at danger or fish with it—its edge is your edge, if you wield it right.")
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
