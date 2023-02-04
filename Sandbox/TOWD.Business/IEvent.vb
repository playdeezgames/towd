Public Interface IEvent
    Sub AssignLink(linkType As LinkType, eventInstance As IEvent)
    Sub AssignInteger(identifier As EventInteger, value As Integer)
    Function GetInteger(identifier As EventInteger) As Integer?
    ReadOnly Property Index As Integer
    ReadOnly Property Link(linkType As LinkType) As IEvent
    ReadOnly Property Links As IEnumerable(Of (LinkType, IEvent))
    ReadOnly Property Integers As IEnumerable(Of (EventInteger, Integer))
    Property EventType As EventType
End Interface
