Friend Class Location
    Inherits LocationDataClient
    Implements ILocation

    Public Sub New(worldData As data.WorldData, locationId As Integer)
        MyBase.New(worldData, locationId)
    End Sub

    Public ReadOnly Property Id As Integer Implements ILocation.Id
        Get
            Return EntityId
        End Get
    End Property

    Public Property EntityType As ILocationType Implements ILocation.EntityType
        Get
            Return EntityData.LocationType.ToDescriptor
        End Get
        Set(value As ILocationType)
            EntityData.LocationType = value.LocationType
            value.Initialize(Me)
        End Set
    End Property

    Public ReadOnly Property Column As Integer Implements ILocation.Column
        Get
            Return EntityData.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ILocation.Row
        Get
            Return EntityData.Row
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ILocation.Map
        Get
            Return New Map(WorldData, EntityData.MapId)
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements ILocation.World
        Get
            Return New World(WorldData)
        End Get
    End Property

    Public Sub AdvanceTime(amount As Integer) Implements ILocation.AdvanceTime
        EntityType.AdvanceTime(Me, amount)
    End Sub
End Class
