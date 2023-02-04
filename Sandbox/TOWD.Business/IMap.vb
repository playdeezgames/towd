Public Interface IMap
    ReadOnly Property Data As MapData
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Sub SetSize(columns As Integer, rows As Integer)
End Interface
