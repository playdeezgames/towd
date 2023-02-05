Public Class WorldShould
    <Fact>
    Public Sub instantiate_a_new_world_with_no_maps()
        Dim subject As IWorld = New World()
        subject.ShouldNotBeNull
        subject.Maps.ShouldBeEmpty
        subject.Avatar.ShouldBeNull
    End Sub
    <Fact>
    Public Sub create_a_map()
        Const MapName = "One"
        Dim subject As IWorld = New World
        Dim map As IMap = subject.CreateMap(MapName)
        map.ShouldNotBeNull
        subject.Maps.ShouldNotBeEmpty
    End Sub
    <Fact>
    Public Sub create_an_event()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        subject.ShouldNotBeNull
    End Sub
    <Fact>
    Public Sub serialize()
        Dim world As IWorld = New World
        Dim subject = world.Serialize()
        subject.ShouldNotBeNull()
    End Sub
    <Fact>
    Public Sub create_avatar()
        Const MapName = "one"
        Const MapColumn = 2
        Const MapRow = 3
        Dim subject As IWorld = New World
        subject.CreateAvatar(MapName, MapColumn, MapRow)
        subject.Avatar.MapName.ShouldBe(MapName)
        subject.Avatar.MapColumn.ShouldBe(MapColumn)
        subject.Avatar.MapRow.ShouldBe(MapRow)
    End Sub
End Class


