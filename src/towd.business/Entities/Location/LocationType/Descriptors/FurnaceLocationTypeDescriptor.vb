Imports System.Text

Friend Class FurnaceLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Furnace, "Furnace")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(data.StatisticType.Fuel, 0)
        location.SetStatisticMinimum(data.StatisticType.Fuel, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Dim builder As New StringBuilder(MyBase.Describe(location))
        builder.AppendLine($"Fuel Remaining: {location.GetStatistic(data.StatisticType.Fuel)}")
        Return builder.ToString
    End Function
End Class
