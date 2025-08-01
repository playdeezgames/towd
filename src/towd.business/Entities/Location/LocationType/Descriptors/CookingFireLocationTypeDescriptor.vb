﻿Imports System.Text

Friend Class CookingFireLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(business.LocationType.CookingFire, "Cooking Fire", "f"c, {StatisticType.Fuel})
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
        location.SetStatistic(StatisticType.Fuel, 9)
        location.SetStatisticMinimum(StatisticType.Fuel, 0)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
        location.ChangeStatistic(StatisticType.Fuel, -1)
        If location.GetStatistic(StatisticType.Fuel) <= 0 Then
            location.EntityType = business.LocationType.Dirt.ToLocationTypeDescriptor
        End If
    End Sub

    Public Overrides Function Describe(location As ILocation) As IEnumerable(Of String)
        Return New List(Of String)(MyBase.Describe(location)) From {
            $"Fuel Remaining: {location.GetStatistic(StatisticType.Fuel)}"
        }
    End Function
End Class
