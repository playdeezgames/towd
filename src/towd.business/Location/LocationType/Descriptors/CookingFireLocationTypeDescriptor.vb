Friend Class CookingFireLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.CookingFire, "Cooking Fire")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub
End Class
