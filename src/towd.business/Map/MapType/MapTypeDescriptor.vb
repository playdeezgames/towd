Imports towd.data

Public Class MapTypeDescriptor
    Implements IMapType

    Sub New(mapType As MapType, name As String, locationType As LocationType)
        Me.MapType = mapType
        Me.Name = name
        Me.LocationType = locationType
    End Sub

    Public ReadOnly Property MapType As MapType Implements IMapType.MapType
    Public ReadOnly Property LocationType As LocationType Implements IMapType.LocationType
    Public ReadOnly Property Name As String Implements IMapType.Name
End Class
