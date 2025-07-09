Imports System.Text

Friend Class CookingFireLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.CookingFire, "Cooking Fire", "f"c)
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.Fuel, 9)
        location.SetStatisticMinimum(StatisticType.Fuel, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
        location.ChangeStatistic(StatisticType.Fuel, -1)
        If location.GetStatistic(StatisticType.Fuel) <= 0 Then
            location.EntityType = data.LocationType.Dirt.ToLocationTypeDescriptor
        End If
    End Sub

    Public Overrides Function Describe(location As ILocation) As String
        Dim builder As New StringBuilder(MyBase.Describe(location))
        builder.AppendLine($"Fuel Remaining: {location.GetStatistic(StatisticType.Fuel)}")
        Return builder.ToString
    End Function
End Class
