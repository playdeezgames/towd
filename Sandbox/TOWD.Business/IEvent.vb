Public Interface IEvent
    Sub AssignLink(linkType As LinkType, eventInstance As IEvent)
    ReadOnly Property Index As Integer
    ReadOnly Property Link(linkType As LinkType) As IEvent
    ReadOnly Property Links As IEnumerable(Of (LinkType, IEvent))
End Interface
