Imports towd.data

Public Class MapTypeDescriptor
    Implements IMapType
    ReadOnly defaultLocationType As LocationType

    Sub New(mapType As MapType, name As String, defaultLocationType As LocationType)
        Me.MapType = mapType
        Me.Name = name
        Me.defaultLocationType = defaultLocationType
    End Sub

    Public ReadOnly Property MapType As MapType Implements IMapType.MapType
    Public ReadOnly Property LocationType As ILocationType Implements IMapType.LocationType
        Get
            Return defaultLocationType.ToDescriptor
        End Get
    End Property
    Public ReadOnly Property Name As String Implements IMapType.Name
End Class
