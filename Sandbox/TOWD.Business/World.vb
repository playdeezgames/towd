Public Class World
    Implements IWorld
    Private ReadOnly _maps As New List(Of IMap)

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return _maps
        End Get
    End Property

    Public Function CreateMap() As IMap Implements IWorld.CreateMap
        Dim map As IMap
        map = New Map()
        _maps.Add(map)
        Return map
    End Function
End Class
