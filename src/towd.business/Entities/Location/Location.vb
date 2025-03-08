Imports towd.data

Friend Class Location
    Inherits Entity(Of ILocationType, LocationData)
    Implements ILocation

    Public Sub New(worldData As data.WorldData, locationId As Integer)
        MyBase.New(worldData, locationId)
    End Sub

    Public Overrides Property EntityType As ILocationType Implements ILocation.EntityType
        Get
            Return EntityData.LocationType.ToDescriptor
        End Get
        Set(value As ILocationType)
            If EntityData.LocationType <> value.LocationType Then
                EntityData.LocationType = value.LocationType
                EntityData.Statistics.Clear()
                EntityData.Flags.Clear()
                value.Initialize(Me)
            End If
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

    Protected Overrides ReadOnly Property EntityData As LocationData
        Get
            Return WorldData.Locations(Id)
        End Get
    End Property

    Public Overrides Sub AdvanceTime(amount As Integer) Implements ILocation.AdvanceTime
        EntityType.AdvanceTime(Me, amount)
    End Sub

    Public Overrides Function ToString() As String
        Return EntityType.Describe(Me)
    End Function
End Class
