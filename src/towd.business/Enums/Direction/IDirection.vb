﻿Public Interface IDirection
    ReadOnly Property Direction As String
    ReadOnly Property Name As String
    Function NextColumn(column As Integer, row As Integer) As Integer
    Function NextRow(column As Integer, row As Integer) As Integer
End Interface
