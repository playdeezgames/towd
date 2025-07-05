Imports System.Text

Friend Class PineLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Pine, "Pine", "P"c)
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
        Dim builder As New StringBuilder(MyBase.Describe(location))
        builder.AppendLine($"Foraging Remaining: {location.GetStatistic(data.StatisticType.ForagingCounter)}")
        builder.AppendLine($"Chopping Remaining: {location.GetStatistic(data.StatisticType.Chopping)}")
        Return builder.ToString
    End Function
End Class
