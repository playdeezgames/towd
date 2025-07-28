Imports System.Text

Friend Class PineLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.Pine, "Pine", "P"c, {StatisticType.ForagingCounter, StatisticType.Chopping})
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.ForagingCounter, 30)
        location.SetStatisticMinimum(StatisticType.ForagingCounter, 0)
        location.SetStatistic(StatisticType.Chopping, 30)
        location.SetStatisticMinimum(StatisticType.Chopping, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As IEnumerable(Of String)
        Return New List(Of String)(MyBase.Describe(location)) From {
            $"Foraging Remaining: {location.GetStatistic(StatisticType.ForagingCounter)}",
            $"Chopping Remaining: {location.GetStatistic(StatisticType.Chopping)}"
        }
    End Function
End Class
