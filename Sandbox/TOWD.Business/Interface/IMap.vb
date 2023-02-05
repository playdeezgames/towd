Public Interface IMap
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Sub SetSize(columns As Integer, rows As Integer)
    Function GetCell(column As Integer, row As Integer) As ICell
    ReadOnly Property Cells As IEnumerable(Of ICell)
    ReadOnly Property World As IWorld
    ReadOnly Property Name As String
End Interface
