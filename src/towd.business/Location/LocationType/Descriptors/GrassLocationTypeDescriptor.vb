Friend Class GrassLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Grass, "Grass")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub
End Class
