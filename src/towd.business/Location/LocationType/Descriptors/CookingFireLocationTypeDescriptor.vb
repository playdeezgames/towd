Friend Class CookingFireLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.CookingFire, "Cooking Fire")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(data.StatisticType.Fuel, 9)
        location.SetStatisticMinimum(data.StatisticType.Fuel, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
        location.ChangeStatistic(data.StatisticType.Fuel, -1)
        If location.GetStatistic(data.StatisticType.Fuel) <= 0 Then
            location.EntityType = data.LocationType.Dirt.ToDescriptor
        End If
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Return $"{MyBase.Describe(location)}(Fuel: {location.GetStatistic(data.StatisticType.Fuel)})"
    End Function
End Class
