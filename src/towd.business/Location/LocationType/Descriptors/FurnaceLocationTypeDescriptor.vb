Friend Class FurnaceLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Furnace, "Furnace")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub
End Class
