Imports System.Text

Friend Class GrassLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.Grass, "Grass", "g"c)
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.ForagingCounter, 30)
        location.SetStatisticMinimum(StatisticType.ForagingCounter, 0)
        location.SetStatistic(StatisticType.Digging, 30)
        location.SetStatisticMinimum(StatisticType.Digging, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Dim builder As New StringBuilder(MyBase.Describe(location))
        builder.AppendLine($"Foraging Remaining: {location.GetStatistic(StatisticType.ForagingCounter)}")
        builder.AppendLine($"Digging Remaining: {location.GetStatistic(StatisticType.Digging)}")
        Return builder.ToString
    End Function
End Class
