Friend Class PondLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Pond, "Pond")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(data.StatisticType.Fishing, 30)
        location.SetStatistic(data.StatisticType.Digging, 30)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Return $"{MyBase.Describe(location)}(Fishing: {location.GetStatistic(data.StatisticType.Fishing)}, Digging: {location.GetStatistic(data.StatisticType.Digging)})"
    End Function
End Class
