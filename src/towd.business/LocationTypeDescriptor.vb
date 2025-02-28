Imports towd.data

Friend Class LocationTypeDescriptor
    Implements ILocationType
    Public ReadOnly Property LocationType As LocationType Implements ILocationType.LocationType

    Public ReadOnly Property Name As String Implements ILocationType.Name
    Sub New(locationType As LocationType, name As String)
        Me.LocationType = locationType
        Me.Name = name
    End Sub
End Class
