Friend Class EventInstance
    Implements IEvent
    Private ReadOnly _worldData As WorldData
    Private ReadOnly _index As Integer
    Friend Sub New(worldData As WorldData, index As Integer)
        _worldData = worldData
        _index = index
    End Sub
    Private ReadOnly Property Data As EventData
        Get
            Return _worldData.Events(_index)
        End Get
    End Property

    Public ReadOnly Property Index As Integer Implements IEvent.Index
        Get
            Return _index
        End Get
    End Property

    Public ReadOnly Property Link(linkType As LinkType) As IEvent Implements IEvent.Link
        Get
            If Not Data.LinkIndices.ContainsKey(linkType) Then
                Return Nothing
            End If
            Return New EventInstance(_worldData, Data.LinkIndices(linkType))
        End Get
    End Property

    Public ReadOnly Property Links As IEnumerable(Of (LinkType, IEvent)) Implements IEvent.Links
        Get
            Return Data.LinkIndices.Select(Function(x)
                                               Dim entry As (LinkType, IEvent) = (x.Key, New EventInstance(_worldData, x.Value))
                                               Return entry
                                           End Function)
        End Get
    End Property

    Public Property EventType As EventType Implements IEvent.EventType
        Get
            Return Data.EventType
        End Get
        Set(value As EventType)
            Data.EventType = value
        End Set
    End Property

    Public ReadOnly Property Integers As IEnumerable(Of (EventInteger, Integer)) Implements IEvent.Integers
        Get
            Return Data.Integers.Select(Function(x)
                                            Dim result As (EventInteger, Integer) = (x.Key, x.Value)
                                            Return result
                                        End Function)
        End Get
    End Property

    Public ReadOnly Property Strings As IEnumerable(Of (EventString, String)) Implements IEvent.Strings
        Get
            Return Data.Strings.Select(Function(x)
                                           Dim result As (EventString, String) = (x.Key, x.Value)
                                           Return result
                                       End Function)
        End Get
    End Property

    Public Sub AssignLink(linkType As LinkType, eventInstance As IEvent) Implements IEvent.AssignLink
        If eventInstance Is Nothing Then
            Data.LinkIndices.Remove(linkType)
            Return
        End If
        Data.LinkIndices(linkType) = eventInstance.Index
    End Sub

    Public Sub AssignInteger(identifier As EventInteger, value As Integer) Implements IEvent.AssignInteger
        Data.Integers(identifier) = value
    End Sub

    Public Function GetInteger(identifier As EventInteger) As Integer? Implements IEvent.GetInteger
        If Not Data.Integers.ContainsKey(identifier) Then
            Return Nothing
        End If
        Return Data.Integers(identifier)
    End Function

    Public Sub AssignString(identifier As EventString, value As String) Implements IEvent.AssignString
        Data.Strings(identifier) = value
    End Sub

    Public Function GetString(identifier As EventString) As String Implements IEvent.GetString
        If Not Data.Strings.ContainsKey(identifier) Then
            Return Nothing
        End If
        Return Data.Strings(identifier)
    End Function
End Class
