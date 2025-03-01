Friend Class RockLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Rock, "Rock")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub
End Class
