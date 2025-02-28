Friend Class Location
    Inherits LocationDataClient
    Implements ILocation

    Public Sub New(worldData As data.WorldData, locationId As Integer)
        MyBase.New(worldData, locationId)
    End Sub

    Public ReadOnly Property Id As Integer Implements ILocation.Id
        Get
            Return locationId
        End Get
    End Property

    Public ReadOnly Property LocationType As ILocationType Implements ILocation.LocationType
        Get
            Return LocationData.LocationType.ToDescriptor
        End Get
    End Property
End Class
