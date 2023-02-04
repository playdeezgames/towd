﻿Public Interface IWorld
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function CreateMap(name As String) As IMap
    Function CreateEvent() As IEvent
End Interface
