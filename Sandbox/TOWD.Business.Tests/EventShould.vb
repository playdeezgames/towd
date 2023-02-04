Public Class EventShould
    <Fact>
    Public Sub have_expected_initial_values()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        subject.ShouldNotBeNull
        subject.Index.ShouldBe(0)
        subject.Links.ShouldBeEmpty
        subject.EventType.ShouldBe(EventType.Message)
        subject.Integers.ShouldBeEmpty
    End Sub
    <Fact>
    Public Sub set_event_type()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        Const expected = EventType.GiveItem
        subject.EventType = expected
        Dim actual = subject.EventType
        actual.ShouldBe(expected)
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
    <Fact>
    Public Sub assign_integer()
        Dim world As IWorld = New World
        Dim subject = world.CreateEvent()
        Const expected = 1
        subject.AssignInteger(EventInteger.Amount, expected)
        subject.Integers.Count.ShouldBe(1)
        Dim actual = subject.GetInteger(EventInteger.Amount)
        actual.ShouldBe(expected)
    End Sub
End Class
