﻿Imports towd.data

Public Interface IMapType
    ReadOnly Property MapType As MapType
    ReadOnly Property Name As String
    ReadOnly Property LocationType As ILocationType
    Sub Initialize(map As IMap)
    Sub AdvanceTime(map As IMap, amount As Integer)
End Interface
