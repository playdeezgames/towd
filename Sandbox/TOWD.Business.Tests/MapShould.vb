Imports TOWD.Data

Public Class MapShould
    <Fact>
    Sub set_size_and_read_size()
        Const MapName = "one"
        Const MapColumns = 2
        Const MapRows = 3
        Dim world As IWorld = New World
        Dim subject = world.CreateMap(MapName)
        subject.SetSize(MapColumns, MapRows)
        subject.Columns.ShouldBe(MapColumns)
        subject.Rows.ShouldBe(MapRows)
        subject.Cells.Count().ShouldBe(MapColumns * MapRows)
    End Sub
    <Theory>
    <InlineData(0, 0)>
    <InlineData(1, 0)>
    <InlineData(0, 2)>
    <InlineData(1, 2)>
    Sub read_cell_from_inside_map(column As Integer, row As Integer)
        Const MapName = "one"
        Const MapColumns = 2
        Const MapRows = 3
        Dim world As IWorld = New World
        Dim subject = world.CreateMap(MapName)
        subject.SetSize(MapColumns, MapRows)
        subject.GetCell(column, row).ShouldNotBeNull()
    End Sub
    <Theory>
    <InlineData(-1, -1)>
    <InlineData(2, -1)>
    <InlineData(-1, 3)>
    <InlineData(2, 3)>
    Sub return_nothing_from_outside_map(column As Integer, row As Integer)
        Const MapName = "one"
        Const MapColumns = 2
        Const MapRows = 3
        Dim world As IWorld = New World
        Dim subject = world.CreateMap(MapName)
        subject.SetSize(MapColumns, MapRows)
        subject.GetCell(column, row).ShouldBeNull()
    End Sub
End Class
