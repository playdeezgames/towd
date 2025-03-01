Friend Class PineLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(data.LocationType.Pine, "Pine")
    End Sub

    Public Overrides Sub Initialize(location As ILocation)
    End Sub
End Class
