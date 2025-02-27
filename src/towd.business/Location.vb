Friend Class Location
    Implements ILocation

    Private ReadOnly worldData As data.WorldData
    Private ReadOnly locationId As Integer

    Public Sub New(worldData As data.WorldData, locationId As Integer)
        Me.worldData = worldData
        Me.locationId = locationId
    End Sub

    Public ReadOnly Property Id As Integer Implements ILocation.Id
        Get
            Return locationId
        End Get
    End Property
End Class
