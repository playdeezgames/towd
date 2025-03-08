Imports towd.data

Friend Class LocationDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property EntityId As Integer
    Protected ReadOnly Property EntityData As LocationData
        Get
            Return WorldData.Locations(EntityId)
        End Get
    End Property

    Public Sub New(worldData As data.WorldData, locationId As Integer)
        MyBase.New(worldData)
        Me.EntityId = locationId
    End Sub
End Class
