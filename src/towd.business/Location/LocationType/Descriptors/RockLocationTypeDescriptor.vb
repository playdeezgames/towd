Friend Class RockLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Rock, "Rock")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(data.StatisticType.Foraging, 30)
        location.SetStatisticMinimum(data.StatisticType.Foraging, 0)
    End Sub


    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub
End Class
