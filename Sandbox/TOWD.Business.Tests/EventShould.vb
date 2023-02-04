Public Class EventShould
    <Fact>
    Public Sub have_expected_initial_values()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        subject.ShouldNotBeNull
    End Sub
    <Fact>
    Public Sub assign_link()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        Dim other = world.CreateEvent()
        subject.AssignLink(LinkType.OnEnter, other)
    End Sub
End Class
