Imports System.Text

Friend Class PondLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.Pond, "Pond", "p"c, {StatisticType.Fishing, StatisticType.Digging})
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.Fishing, 30)
        location.SetStatistic(StatisticType.Digging, 30)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As IEnumerable(Of String)
        Return New List(Of String)(MyBase.Describe(location)) From {
            $"Fishing Remaining: {location.GetStatistic(StatisticType.Fishing)}",
            $"Digging Remaining: {location.GetStatistic(StatisticType.Digging)}"
        }
    End Function
End Class
