Imports System.Diagnostics.CodeAnalysis

Friend Class EventInstance
    Implements IEvent
    Private _worldData As WorldData
    Private _index As Integer
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

    Public Sub AssignLink(linkType As LinkType, eventInstance As IEvent) Implements IEvent.AssignLink
        If eventInstance Is Nothing Then
            Data.LinkIndices.Remove(linkType)
            Return
        End If
        Data.LinkIndices(linkType) = eventInstance.Index
    End Sub
End Class
