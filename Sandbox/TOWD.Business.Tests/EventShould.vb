Public Class EventShould
    <Fact>
    Public Sub have_expected_initial_values()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        subject.ShouldNotBeNull
        subject.Index.ShouldBe(0)
        subject.Links.ShouldBeEmpty
    End Sub
    <Fact>
    Public Sub assign_link()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        Dim other = world.CreateEvent()
        subject.AssignLink(LinkType.OnEnter, other)
        subject.Links.Count.ShouldBe(1)
        Dim actual = subject.Link(LinkType.OnEnter)
        actual.Index.ShouldBe(other.Index)
    End Sub
End Class
