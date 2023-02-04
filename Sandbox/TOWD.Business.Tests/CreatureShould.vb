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
End Class
