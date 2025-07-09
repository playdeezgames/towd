Imports towd.data

Public MustInherit Class MapTypeDescriptor
    Implements IMapType
    ReadOnly defaultLocationType As LocationType

    Sub New(
           mapType As MapType,
           name As String,
           spawnCount As Integer,
           columns As Integer,
           rows As Integer,
           defaultLocationType As LocationType)
        Me.MapType = mapType
        Me.Name = name
        Me.defaultLocationType = defaultLocationType
        Me.Columns = columns
        Me.Rows = rows
        Me.SpawnCount = spawnCount
    End Sub

    Public ReadOnly Property MapType As MapType Implements IMapType.MapType
    Public ReadOnly Property LocationType As ILocationType Implements IMapType.LocationType
        Get
            Return defaultLocationType.ToLocationTypeDescriptor
        End Get
    End Property
    Public ReadOnly Property Name As String Implements IMapType.Name

    Public ReadOnly Property Columns As Integer Implements IMapType.Columns

    Public ReadOnly Property Rows As Integer Implements IMapType.Rows

    Public ReadOnly Property SpawnCount As Integer Implements IMapType.SpawnCount

    Public MustOverride Sub Initialize(map As IMap) Implements IMapType.Initialize
    Protected MustOverride Sub OnAdvanceTime(amount As Integer)
    Public Sub AdvanceTime(map As IMap, amount As Integer) Implements IMapType.AdvanceTime
        OnAdvanceTime(amount)
        For Each location In map.Locations
            location.AdvanceTime(amount)
        Next
    End Sub
End Class
