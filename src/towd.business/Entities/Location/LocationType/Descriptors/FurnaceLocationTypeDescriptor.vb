Imports System.Text

Friend Class FurnaceLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.Furnace, "Furnace", "F"c, {StatisticType.Fuel})
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.Fuel, 0)
        location.SetStatisticMinimum(StatisticType.Fuel, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As IEnumerable(Of String)
        Return New List(Of String)(MyBase.Describe(location)) From {
            $"Fuel Remaining: {location.GetStatistic(StatisticType.Fuel)}"
        }
    End Function
End Class
