Imports System.Text

Friend Class PondLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Pond, "Pond", "p"c)
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.Fishing, 30)
        location.SetStatistic(StatisticType.Digging, 30)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Dim builder As New StringBuilder(MyBase.Describe(location))
        builder.AppendLine($"Fishing Remaining: {location.GetStatistic(StatisticType.Fishing)}")
        builder.AppendLine($"Digging Remaining: {location.GetStatistic(StatisticType.Digging)}")
        Return builder.ToString
    End Function
End Class
