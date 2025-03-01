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

    Public Property EntityType As ILocationType Implements ILocation.EntityType
        Get
            Return LocationData.LocationType.ToDescriptor
        End Get
        Set(value As ILocationType)
            LocationData.LocationType = value.LocationType
            value.Initialize(Me)
        End Set
    End Property

    Public ReadOnly Property Column As Integer Implements ILocation.Column
        Get
            Return LocationData.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ILocation.Row
        Get
            Return LocationData.Row
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ILocation.Map
        Get
            Return New Map(WorldData, LocationData.MapId)
        End Get
    End Property

    Public Sub AdvanceTime(amount As Integer) Implements ILocation.AdvanceTime
        EntityType.AdvanceTime(Me, amount)
    End Sub
End Class
