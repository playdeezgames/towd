Friend Class GrassLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Grass, "Grass")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub

    Public Overrides Sub AdvanceTime(location As ILocation, amount As Integer)
    End Sub
End Class
