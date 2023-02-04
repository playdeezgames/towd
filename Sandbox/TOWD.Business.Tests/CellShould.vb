Public Class CellShould
    <Fact>
    Public Sub read_terrain_type()
        Const MapName = "one"
        Const MapColumns = 2
        Const MapRows = 3
        Dim world As IWorld = New World
        Dim map = world.CreateMap(MapName)
        map.SetSize(MapColumns, MapRows)
        Dim cell = map.GetCell(0, 0)
        cell.TerrainType.ShouldBe(TerrainType.None)
    End Sub
    <Fact>
    Public Sub write_terrain_type()
        Const MapName = "one"
        Const MapColumns = 2
        Const MapRows = 3
        Const expected = TerrainType.Bed
        Dim world As IWorld = New World
        Dim map = world.CreateMap(MapName)
        map.SetSize(MapColumns, MapRows)
        Dim cell = map.GetCell(0, 0)
        cell.TerrainType = expected
        cell.TerrainType.ShouldBe(expected)
    End Sub
End Class
