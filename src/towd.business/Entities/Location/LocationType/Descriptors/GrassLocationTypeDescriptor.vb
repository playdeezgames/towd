Imports System.Text

Friend Class GrassLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.Grass, "Grass", "g"c, {StatisticType.ForagingCounter, StatisticType.Digging})
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.ForagingCounter, 30)
        location.SetStatisticMinimum(StatisticType.ForagingCounter, 0)
        location.SetStatistic(StatisticType.Digging, 30)
        location.SetStatisticMinimum(StatisticType.Digging, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As IEnumerable(Of String)
        Return New List(Of String)(MyBase.Describe(location)) From {
            $"Foraging Remaining: {location.GetStatistic(StatisticType.ForagingCounter)}",
            $"Digging Remaining: {location.GetStatistic(StatisticType.Digging)}"
        }
    End Function
End Class
