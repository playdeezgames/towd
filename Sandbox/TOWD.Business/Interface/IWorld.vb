Public Interface IWorld
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function CreateMap(name As String) As IMap
    Function CreateEvent() As IEvent
    Function Serialize() As String
    Sub CreateAvatar(name As String, column As Integer, row As Integer)
    ReadOnly Property Avatar As IAvatar
End Interface
