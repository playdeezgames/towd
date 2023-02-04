﻿Imports TOWD.Data

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
        subject.Data.Cells.Count().ShouldBe(MapColumns * MapRows)
    End Sub
End Class
