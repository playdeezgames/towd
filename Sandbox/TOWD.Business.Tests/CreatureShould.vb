Public Class CreatureShould
    <Fact>
    Public Sub have_expected_initial_values()
        Dim world As IWorld = New World
        Dim map = world.CreateMap("test")
        map.SetSize(1, 1)
        Dim cell = map.GetCell(0, 0)
        Dim subject = cell.CreateCreature()
        subject.CreatureType.ShouldBe(CreatureType.Dude)
        subject.OnInteract.ShouldBeNull
    End Sub
    <Fact>
    Public Sub set_creature_type()
        Dim world As IWorld = New World
        Dim map = world.CreateMap("test")
        map.SetSize(1, 1)
        Dim cell = map.GetCell(0, 0)
        Const expected = CreatureType.Guard
        Dim subject = cell.CreateCreature()
        subject.CreatureType = expected
        subject.CreatureType.ShouldBe(expected)
    End Sub
    <Fact>
    Public Sub set_on_interact_event()
        Dim world As IWorld = New World
        Dim map = world.CreateMap("test")
        map.SetSize(1, 1)
        Dim cell = map.GetCell(0, 0)
        Dim subject = cell.CreateCreature()
        Dim expected = world.CreateEvent
        subject.OnInteract = expected
        Dim actual = subject.OnInteract
        actual.ShouldNotBeNull
        actual.Index.ShouldBe(expected.Index)
    End Sub
End Class
