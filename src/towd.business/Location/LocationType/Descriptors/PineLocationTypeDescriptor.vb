﻿Friend Class PineLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Pine, "Pine")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(data.StatisticType.ForagingCounter, 30)
        location.SetStatisticMinimum(data.StatisticType.ForagingCounter, 0)
        location.SetStatistic(data.StatisticType.Chopping, 30)
        location.SetStatisticMinimum(data.StatisticType.Chopping, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Return $"{MyBase.Describe(location)}(Foraging: {location.GetStatistic(data.StatisticType.ForagingCounter)}, Chopping: {location.GetStatistic(data.StatisticType.Chopping)})"
    End Function
End Class
